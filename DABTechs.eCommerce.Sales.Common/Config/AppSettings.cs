using System;
using System.Text;
using Newtonsoft.Json;

namespace DABTechs.eCommerce.Sales.Common.Config
{
    public class AppSettings
    {
        [JsonProperty("LocalDomain")]
        public string LocalDomain { get; set; }

        [JsonProperty("VIPDomain")]
        public string VIPDomain { get; set; }

        [JsonProperty("SaleDomain")]
        public string SaleDomain { get; set; }

        [JsonProperty("ClearanceDomain")]
        public string ClearanceDomain { get; set; }

        [JsonProperty("ProductsPerPage")]
        public int ProductsPerPage { get; set; }

        [JsonProperty("SearchProviderName")]
        public string SearchProviderName { get; set; }

        [JsonProperty("ImageBaseUrl")]
        public string ImageBaseUrl { get; set; }

        [JsonProperty("AzureApi")]
        public string AzureApi { get; set; }

        [JsonProperty("BloomreachApi")]
        public string BloomreachApi { get; set; }

        [JsonIgnore]
        public string BloomreachApiSettingsValue
        {
            get
            {
                var brSettings = new StringBuilder();
                brSettings.Append($"account_id={BloomreachAccountId}");
                brSettings.Append($"&auth_key={BloomreachAuthKey}");
                brSettings.Append($"&domain_key={BloomreachDomainKey}");
                brSettings.Append($"&request_id={DateTime.Now.ToString("yyyyMMddHHmmssfffffff")}");
                brSettings.Append($"&&_br_uid_2={BloomreachBRUID}");
                brSettings.Append($"&url={BloomreachUrl}");
                brSettings.Append($"&ref_url={BloomreachRefUrl}");
                brSettings.Append($"&request_type={BloomreachRequestType}");
                brSettings.Append($"&facet.limit={BloomreachFacetLimit}");
                brSettings.Append($"&fl={BloomreachFL}");
                brSettings.Append($"&stats.field={Statistics}");

                return brSettings.ToString();
            }
        }

        [JsonProperty("BloomreachAccountId")]
        public string BloomreachAccountId { get; set; }

        [JsonProperty("BloomreachAuthKey")]
        public string BloomreachAuthKey { get; set; }

        [JsonProperty("BloomreachDomainKey")]
        public string BloomreachDomainKey { get; set; }

        [JsonProperty("BloomreachBRUID")]
        public string BloomreachBRUID { get; set; }

        [JsonProperty("BloomreachRequestId")]
        public string BloomreachRequestId { get; set; }

        [JsonProperty("BloomreachUrl")]
        public string BloomreachUrl { get; set; }

        [JsonProperty("BloomreachRefUrl")]
        public string BloomreachRefUrl { get; set; }

        [JsonProperty("BloomreachRequestType")]
        public string BloomreachRequestType { get; set; }

        [JsonProperty("BloomreachFacetLimit")]
        public string BloomreachFacetLimit { get; set; }

        [JsonProperty("Statistics")]
        public string Statistics { get; set; }

        [JsonProperty("BloomreachFL")]
        public string BloomreachFL { get; set; }

        [JsonProperty("AuthCookieSecretKey")]
        public string AuthCookieSecretKey { get; set; }

        [JsonProperty("FiltersHtmlString")]
        public string FiltersHtmlString { get; set; }

        [JsonProperty("SearchResultsHtmlString")]
        public string SearchResultsHtmlString { get; set; }

        [JsonProperty("RemoteServer")]
        public string RemoteServer { get; set; }

        [JsonProperty("LocalServer")]
        public string LocalServer { get; set; }

        [JsonProperty("TemplateCacheMins")]
        public int TemplateCacheMins { get; set; }

        [JsonProperty("DemandPublicKey")]
        public bool DemandPublicKey { get; set; }

        [JsonProperty("RemoteDomain")]
        public string RemoteDomain { get; set; }

        [JsonProperty("CookieDomain")]
        public string CookieDomain { get; set; }

        [JsonProperty("UseProxy")]
        public bool UseProxy { get; set; }

        [JsonProperty("ProxyAddress")]
        public string ProxyAddress { get; set; }

        [JsonProperty("LocalResource")]
        public string LocalResource { get; set; }

        [JsonProperty("MaxPages")]
        public int MaxPages { get; set; }
    }
}