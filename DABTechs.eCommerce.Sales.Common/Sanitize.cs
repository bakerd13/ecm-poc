using System.Text.RegularExpressions;

namespace DABTechs.eCommerce.Sales.Common
{
    public static class StringHelper
    {
        public static string Sanitize(this string value, bool replaceSpaces = false)
        {
            var result = value ?? string.Empty;
            if (replaceSpaces)
            {
                result = Regex.Replace(result, " ", "_", RegexOptions.Compiled);
            }
            result = Regex.Replace(result, "[^0-9A-Za-z_]+", "", RegexOptions.Compiled).ToLower();

            return result;
        }
    }
}