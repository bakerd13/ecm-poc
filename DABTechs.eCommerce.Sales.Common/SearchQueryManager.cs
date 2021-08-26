using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace DABTechs.eCommerce.Sales.Common
{
    public static class SearchQueryManager
    {
        private static readonly object objectLocker = new object();

        private static string FilterConfig
        {
            get
            {
                return ConfigurationManager.AppSettings["FilterMappings"];
            }
        }

        private static List<FilterMapping> filterMappings;

        public static List<FilterMapping> FilterMapping
        {
            get
            {
                if (filterMappings != null) { return filterMappings; }

                lock (objectLocker)
                {
                    filterMappings = new List<FilterMapping>();
                    if (FilterConfig != null)
                    {
                        string[] filters = FilterConfig.Split('|');
                        foreach (string filter in filters)
                        {
                            string[] filterParts = filter.Split(':');
                            if (filterParts.Length < 3) { continue; }
                            string filterKey = filterParts[0];
                            string filterValue = filterParts[1];
                            string filterReplacement = filterParts[2];

                            filterMappings.Add(new FilterMapping() { Name = filterKey, Original = filterValue, Replacement = filterReplacement });
                        }
                    }
                }

                return filterMappings;
            }
        }

        public static string Map(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(value)) { return value; }
            var result = FilterMapping.FirstOrDefault(n => string.Equals(n.Name, name, StringComparison.InvariantCultureIgnoreCase) && string.Equals(n.Original, value, StringComparison.InvariantCultureIgnoreCase));
            if (result == null)
            {
                return value;
            }
            else
            {
                return result.Replacement;
            }
        }
    }
}