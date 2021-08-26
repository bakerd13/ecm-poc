using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using DABTechs.eCommerce.Sales.Business.Interfaces;
using DABTechs.eCommerce.Sales.Common;
using DABTechs.eCommerce.Sales.Common.Config;

namespace DABTechs.eCommerce.Sales.Business
{
    public class AuthenticationService : IAuthenticationService
    {
        internal const string COOKIE_IV_KEY = "Version";

        internal readonly byte[] ENCRYPTION_KEY = new byte[] {  0x6d, 0x31, 0xd5, 0xd5, 0x9c, 0xdb, 0x10, 0x64,
                                                        0xcd, 0x7f, 0x22, 0xf5, 0x05, 0xbd, 0xa9, 0x9f,
                                                        0x0d, 0xe8, 0x94, 0xf8, 0x62, 0x21, 0x00, 0x79,
                                                        0x14, 0x7a, 0x86, 0xd0, 0x4a, 0xfa, 0x4f, 0xde  };

        /// <summary>
        /// The Account Number of the logged in customer
        /// </summary>
        public string AccountNo
        {
            get;
            set;
        }

        /// <summary>
        /// The time of the login (in ticks)
        /// </summary>
        public long Timestamp
        {
            get;
            set;
        }

        /// <summary>
        /// Boolean to indicate if the NEXT-AP cookie has been found (and read).
        /// </summary>
        public bool CookieFound
        {
            get;
            set;
        }

        /// <summary>
        /// The Source Site of the cookie (Main or Sale)
        /// </summary>
        public string SourceSite
        {
            get;
            set;
        }

        public string AuthToken { get; set; }
        public bool ReadVIPSalePopUpModal { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// The constructor method reads in the NextSaleAuth cookie, if present.
        /// </summary>
        public AuthenticationService(IHttpContextAccessor httpContextAccessor, IOptions<AppSettings> appSettings)
        {
            CookieFound = false;
            SourceSite = "Main";
            ReadVIPSalePopUpModal = false;
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appSettings.Value;

            // if cookie exists, read in the data and assign to private variables.
            var saleAuthCookie = _httpContextAccessor?.HttpContext.Request.Cookies["NextSaleAuth"];
            if (!string.IsNullOrWhiteSpace(saleAuthCookie))
            {
                // Read account number
                AccountNo = CookieValues.CookieKeyValue<string>(saleAuthCookie, "AccountNo");

                // If the lengh of the account number is longer than 8 assume encryted and decrypt.
                if (saleAuthCookie.Contains(COOKIE_IV_KEY))
                {
                    var dataString = Uri.UnescapeDataString(CookieValues.CookieKeyValue<string>(saleAuthCookie, COOKIE_IV_KEY));
                    var security = new Security(ENCRYPTION_KEY, Convert.FromBase64String(dataString));
                    AccountNo = security.Decrypt(Convert.FromBase64String(Uri.UnescapeDataString(AccountNo)));
                }

                AuthToken = CookieValues.CookieKeyValue<string>(saleAuthCookie, "AuthToken");
                //if the auth token comes from classic asp then some values are encoded
                if (AuthToken.Contains("%"))
                {
                    AuthToken = AuthToken.Replace("%2F", "/");
                    AuthToken = AuthToken.Replace("%3D", "=");
                }
                SourceSite = CookieValues.CookieKeyValue<string>(saleAuthCookie, "SourceSite");
                if (long.TryParse(CookieValues.CookieKeyValue<string>(saleAuthCookie, "Timestamp"), out long timestamp)) Timestamp = timestamp;
                if (bool.TryParse(CookieValues.CookieKeyValue<string>(saleAuthCookie, "ReadVIPSalePopUpModal"), out bool readVIPSalePopUpModal)) ReadVIPSalePopUpModal = readVIPSalePopUpModal;
                CookieFound = true;
            }
        }

        /// <summary>
        /// Write everything to the NextSaleAuth cookie and create it as a Session only cookie;</c>
        /// </summary>
        public void WriteCookie()
        {
            // Encrypt the account number and store in cookie.
            if (!string.IsNullOrWhiteSpace(AccountNo))
            {
                var security = new Security(ENCRYPTION_KEY);
                _httpContextAccessor.HttpContext.Response.Cookies.Append("NextSaleAuth", Convert.ToBase64String(security.Encrypt(AccountNo)));
                _httpContextAccessor.HttpContext.Response.Cookies.Append("NextSaleAuth", Convert.ToBase64String(security.IV));
            }
            else
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Append("NextSaleAuth", AccountNo);
            }

            Timestamp = DateTime.UtcNow.Ticks;
            string secretKey = _appSettings.AuthCookieSecretKey;
            string newToken = AccountNo + secretKey + Timestamp.ToString();
            BinaryFormatter bf = new BinaryFormatter();
            byte[] bytes;
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, newToken);
            ms.Seek(0, 0);
            bytes = ms.ToArray();
            using (SHA256 shaM = new SHA256Managed())
            {
                AuthToken = Convert.ToBase64String(shaM.ComputeHash(bytes));
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append("NextSaleAuth", AuthToken);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("NextSaleAuth", Timestamp.ToString());
            _httpContextAccessor.HttpContext.Response.Cookies.Append("NextSaleAuth", SourceSite);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("NextSaleAuth", ReadVIPSalePopUpModal.ToString());

            ms.Dispose();
        }

        public bool AuthCheck()
        {
            if (!CookieFound) { return false; }

            string secret = _appSettings.AuthCookieSecretKey;

            string newToken = AccountNo + secret + Timestamp.ToString();
            BinaryFormatter bf = new BinaryFormatter();
            byte[] bytes;
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, newToken);
            ms.Seek(0, 0);
            bytes = ms.ToArray();
            byte[] result;
            using (SHA256 shaM = new SHA256Managed())
            {
                result = shaM.ComputeHash(bytes);
            }

            ms.Dispose();
            return (Convert.ToBase64String(result) == AuthToken);
        }

        public void ClearCookie()
        {
            //set the values of the cookie to null and set its expiration time to be in the past.
            if (_httpContextAccessor.HttpContext.Response.Cookies != null)
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Append("NextSaleAuth", "");
                _httpContextAccessor.HttpContext.Response.Cookies.Append("NextSaleAuth", "0");
                _httpContextAccessor.HttpContext.Response.Cookies.Append("NextSaleAuth", "Main");
                _httpContextAccessor.HttpContext.Response.Cookies.Append("NextSaleAuth", "false");
                _httpContextAccessor.HttpContext.Response.Cookies.Append("NextSaleAuth", DateTime.UtcNow.AddDays(-7d).ToString());
            }
        }
    }
}