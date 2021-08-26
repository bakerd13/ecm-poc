using System;

namespace DABTechs.eCommerce.Sales.Common
{
    public static class CookieValues
    {
        /// <summary>
        /// Gets the cookie value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cookie">The cookie.</param>
        /// <param name="key">The key.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns></returns>
        public static T CookieKeyValue<T>(string cookie, string key, char separator = '&', char delimiter = '=')
        {
            if (string.IsNullOrWhiteSpace(cookie)) { return default; }

            var cookieParts = cookie.Split(separator);
            foreach (var cookiePart in cookieParts)
            {
                var keyParts = cookiePart.Split(delimiter);
                if (keyParts.Length <= 0) { continue; }
                foreach (var keyPart in keyParts)
                {
                    if (string.Equals(keyParts[0], key, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return (T)Convert.ChangeType(keyParts[1], typeof(T));
                    }
                }
            }

            return default;
        }
    }
}