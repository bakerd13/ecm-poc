using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DABTechs.eCommerce.Sales.Common
{
    public static class UrlHelper
    {
        /// <summary>Parameters values are added in the Querystring in 2 possible formats, ie: size=1  OR size=1|2</summary>
        /// <param name="value"></param>
        /// <returns>True if Key name found, otherwise False</returns>
        public static bool ParamExist(string queryString, string facetName, string value)
        {
            switch (facetName.ToLower())
            {
                case "producttype":
                    facetName = "category";
                    break;

                case "options":
                    facetName = "size";
                    break;

                case "savingnumber":
                    facetName = "saving";
                    break;

                default:
                    break;
            }
            string[] keyParam = GetParameterValue(queryString, facetName).Split('=');   //## We get the Values of the specific Parameter, ie: Brand, Colour...

            if (string.IsNullOrEmpty(keyParam[0]))    //## When no Params available on the URL at all, will not match anything
            {
                return false;
            }

            if (keyParam[1].Contains("|"))
            {
                string[] keyValues = keyParam[1].Split('|');
                return (keyValues.Contains(value));
            }
            else
            {
                return keyParam[1].Equals(value);
            }
        }

        /// <summary>Will look for the specified Keyname in the QueryString and pass the key pair value</summary>
        /// <param name="keyName">Key Name in the URL</param>
        /// <returns>Key and Value, ie: '&page=3'</returns>
        public static string GetParameterValue(string queryString, string keyName)
        {
            Regex regex = new Regex($"{keyName}=([^&#]*)", RegexOptions.IgnoreCase);
            Match match = regex.Match(queryString);

            return match.Success ? HttpUtility.UrlDecode(match.Value) : "";
        }

        public static List<KeyValuePair<string, string>> GetFilterCollection(string afParameterValue)
        {
            var filterCollection = new List<KeyValuePair<string, string>>();
            if (string.IsNullOrWhiteSpace(afParameterValue)) return filterCollection;

            var filtersString = afParameterValue.Trim().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var filterString in filtersString)
            {
                var filterSplit = filterString.Split(':');
                filterCollection.Add(new KeyValuePair<string, string>(filterSplit[0], filterSplit[1].Replace("_", " ")));
            }

            return filterCollection;
        }
    }
}