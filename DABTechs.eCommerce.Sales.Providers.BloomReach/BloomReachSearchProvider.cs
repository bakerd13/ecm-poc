using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using DABTechs.eCommerce.Sales.Common;
using DABTechs.eCommerce.Sales.Common.Config;
using DABTechs.eCommerce.Sales.Common.Enums;
using DABTechs.eCommerce.Sales.Domain;
using DABTechs.eCommerce.Sales.Business.Collections;
using DABTechs.eCommerce.Sales.Providers.BloomReach.Mapper;
using DABTechs.eCommerce.Sales.Providers.BloomReach.Models;
using DABTechs.eCommerce.Sales.Business.Interfaces;
using DABTechs.eCommerce.Sales.Business;
using DABTechs.eCommerce.Sales.Business.Models.Search;
using DABTechs.eCommerce.Sales.Models.Search;

namespace DABTechs.eCommerce.Sales.Providers.BloomReach
{
    /// <summary>
    /// The BloomReach Search Provider.
    /// </summary>
    /// <seealso cref="ISearchProvider" />
    public class BloomReachSearchProvider : ISearchProvider, IDisposable
    {
        private static readonly object _objectLocker = new object();
        private static DateTime? _timeStampCheck = null;
        private readonly AppSettings _appSettings;
        private readonly BloomReachHttpClient _bloomReachProvider;
        private readonly MemoryCache _memoryCache;
        private bool disposed;

        public BloomReachSearchProvider(BloomReachHttpClient client, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _bloomReachProvider = client;
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
        }

        public SearchProvider SearchProviderType { get => SearchProvider.BloomReach; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets the raw result string from the search provider.
        /// </summary>
        /// <param name="searchQuery">The search query.</param>
        /// <returns></returns>
        public async Task<string> GetApiResult(FilterQuery filterQuery)
        {
            try
            {
                // Build the Blomreach Spec API Url
                string bloomReachRequest = BuildBloomReachRequest(filterQuery);
                string brApi = $"?{_appSettings.BloomreachApiSettingsValue}{bloomReachRequest}";

                // Fetch the BloomReach Response (async)
                HttpResponseMessage response = await _bloomReachProvider.Client.GetAsync(brApi).ConfigureAwait(false);

                // Ensure we have a valid response.
                if (!response.IsSuccessStatusCode)
                {
                    Logger.Error($"Invalid Response From BloomReach. { response.ReasonPhrase } { response.StatusCode }");
                    return null;
                }

                // Fetch the raw response. (async)
                string rawData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                // Return the raw data.
                return rawData;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception whilst calling BloomReach. { ex.Message }");
                throw;
            }
        }

        public SearchResults Map(string rawResult)
        {
            try
            {
                BloomReachSearchResultMapper mapper = new BloomReachSearchResultMapper(_appSettings);
                SearchResults result = mapper.Map(rawResult);

                // TODO Apply Timestamp
                // ApplyBloomReachTimeStamp(result);

                return result;
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Failed to map bloomreach search result: {ex.Message}");
                throw;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                _memoryCache.Dispose();
            }
            disposed = true;
        }

        /// <summary>This will read the Generic Param and return BR secific API Value for Sort Param</summary>
        /// <param name="sortValue">Sort Method</param>
        /// <returns>Complete API Value for Sort</returns>
        private static string BuildSortByParam(string sortValue)
        {
            //## No param for Sort means= 'bst' or 'Most popular Items'- which will be produced by BR

            if (sortValue.Equals("price"))
            {
                return "&sort=sale_price+asc";
            }

            if (sortValue.Equals("price rev"))
            {
                return "&sort=sale_price+desc";
            }

            return "&total_stock_balance+desc";
        }

        private static string GetFqFromFilterCollection(List<KeyValuePair<string, string>> filterCollection)
        {
            var builder = new StringBuilder();

            foreach (var filterGroup in filterCollection.GroupBy(c => c.Key))
            {
                var filterName = filterGroup.Key;

                //## Bloomreach specific names
                switch (filterName)
                {
                    case "savings":
                        filterName = "discount";
                        break;

                    case "si":
                        filterName = "sizes";
                        break;

                    case "sub":
                        filterName = "product_type";
                        break;

                    case "cat":
                        filterName = "department";
                        break;
                }

                if (filterName == "price")
                {
                    //## we will get something like: 'price:10-20', change it to "&fq=price:[10 TO 20]"
                    builder.Append("&fq=sale_price:[");

                    var priceValue = filterGroup.Select(f => f.Value).First();

                    var rangeValues = priceValue.Replace("[", "").Replace("]", "").Split(',');

                    var min = int.TryParse(rangeValues[0], out var result) ? (result / 100).ToString() : "*";
                    var max = int.TryParse(rangeValues[1], out result) ? (result / 100).ToString() : "*";

                    builder.Append($"{min} TO {max}]");
                }
                else
                {
                    builder.Append($"&fq={filterName}:");

                    var filterValues = filterGroup.Select(f => f.Value).ToList();

                    foreach (var value in filterValues)
                    {
                        builder.Append($"\"{value}\""); //## Looks like=> "&fq=color:"red"&fq=brand:"Rebel in Rose"&fq=color: ("red" OR "purple")

                        if (!filterValues.Last().Equals(value))
                        {
                            builder.Append("OR");
                        }
                    }
                }
            }

            return builder.ToString();
        }

        private string GetCategoriesFromFilterQuery(List<AfValue> categorySearch)
        {
            var builder = new StringBuilder();

            foreach (var name in categorySearch.Select(x => x.Name).Distinct())
            {
                string filterName = TranslatedFilterNamee(name);
                builder.Append($"&fq={filterName}:");
                var categoryValues = categorySearch.Where(w => w.Name == name).Select(x => x.Value);

                foreach (var value in categoryValues)
                {
                    builder.Append($"\"{value}\""); //## Looks like=> "&fq=color:"red"&fq=brand:"Rebel in Rose"&fq=color: ("red" OR "purple")

                    if (!categoryValues.Last().Equals(value))
                    {
                        builder.Append("OR");
                    }
                }
            }

            return builder.ToString();
        }

        private string GetSelectedFiltersFromFilterQuery(List<AfValue> filters)
        {
            var builder = new StringBuilder();

            foreach (var name in filters.Select(x => x.Name).Distinct())
            {
                string filterName = TranslatedFilterNamee(name);
                builder.Append($"&fq={filterName}:");
                var filtersValues = filters.Where(w => w.Name == name).Select(x => x.Value);

                foreach (var value in filtersValues)
                {
                    builder.Append($"\"{value}\""); //## Looks like=> "&fq=color:"red"&fq=brand:"Rebel in Rose"&fq=color: ("red" OR "purple")

                    if (!filtersValues.Last().Equals(value))
                    {
                        builder.Append("OR");
                    }
                }
            }

            return builder.ToString();
        }

        private static string TranslatedFilterNamee(string name)
        {
            var filterName = name;
            //## Bloomreach specific names
            switch (name)
            {
                case "savings":
                    filterName = "discount";
                    break;
                case "si":
                    filterName = "sizes";
                    break;

                case "sub":
                    filterName = "product_type";
                    break;

                case "cat":
                    filterName = "department";
                    break;
            }

            return filterName;
        }

        private string GetPricesFromFilterQuery(PriceValue prices)
        {
            var builder = new StringBuilder();

            //## we will get something like: 'price:10-20', change it to "&fq=price:[10 TO 20]"
            builder.Append("&fq=sale_price:[");

            var min = prices.PriceMin != prices.PriceFrom ? prices.PriceFrom.ToString() : "*";
            var max = prices.PriceMax != prices.PriceTo ? prices.PriceTo.ToString() : "*";

            builder.Append($"{min} TO {max}]");

            return builder.ToString();
        }

        // TODO Reuse this get refs from backup
        //private void ApplyBloomReachTimeStamp(ISearchActionResult results)
        //{
        //    try
        //    {
        //        // Ensure we have a results and pagination.
        //        if (results == null || results.Pagination == null) { return; }

        //        // Attempt to get the timestamp from memorycache.
        //        TimeStamp timeStamp = _memoryCache.Get("BloomReach_TimeStamp") as TimeStamp;

        //        // Apply the existing timestamp to the pagination results.
        //        if (timeStamp != null)
        //        {
        //            results.Pagination.SetTimestamp(timeStamp.Date, "BR");
        //        }

        //        // Determine if we need to call bloomreach for timestamp.
        //        if (timeStamp == null || _timeStampCheck == null || DateTime.Now > _timeStampCheck.Value.AddMinutes(3))
        //        {
        //            lock (_objectLocker)
        //            {
        //                _timeStampCheck = DateTime.Now;

        //                // Build the Blomreach Spec API Url
        //                string bloomReachRequest = BuildBloomReachRequest(new SearchQuery() { W = "*" });
        //                string brApi = $"?{_appSettings.BloomreachApiSettingsValue}{bloomReachRequest}";

        //                // Fetch the BloomReach Response (async)
        //                HttpResponseMessage response = _bloomReachProvider.Client.GetAsync(brApi).Result;
        //                if (!response.IsSuccessStatusCode) { return; }

        //                // Call BloomReach for a * search.
        //                BloomReachModel bloomReachModel = JsonConvert.DeserializeObject<BloomReachModel>(response.Content.ReadAsStringAsync().Result);
        //                if (bloomReachModel == null) { return; }

        //                // Construct the new timestamp object.
        //                timeStamp = new TimeStamp
        //                {
        //                    Date = Dates.GetDateTimeFromUnixFormat(bloomReachModel.Stats.StatFields.LastUpdated.MaximumLastUpdated)
        //                };

        //                // Set the pagination result.
        //                results.Pagination.SetTimestamp(timeStamp.Date, "BR");

        //                _memoryCache.Set("BloomReach_TimeStamp", timeStamp);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Trace.WriteLine($"Failed to apply BloomReach timestamp { ex.Message }");
        //    }
        //}

        private string BuildBloomReachRequest(FilterQuery filterQuery)
        {
            if (string.IsNullOrWhiteSpace(filterQuery.KeywordSearch))
            {
                filterQuery.KeywordSearch = "*";
            }

            var builder = new StringBuilder();

            //override * keyword search
            if (filterQuery.KeywordSearch == "*")
            {
                builder.Append("&search_type=category&q=1");
            }
            else
            {
                // If the query is an item number with a hypen in, remove all hyphens from the string
                var itemNumberRegex = new Regex(@"([a-zA-Z0-9]{3}-[a-zA-Z0-9]{3})$");
                if (itemNumberRegex.IsMatch(filterQuery.KeywordSearch))
                {
                    filterQuery.KeywordSearch = filterQuery.KeywordSearch.Replace("-", "");
                }

                builder.Append($"&search_type=keyword&q={filterQuery.KeywordSearch}");
            }

            string pageNumberParams = BuildUrlPagingParam(filterQuery.PageNumber.ToString());
            string pageSortParams = BuildSortByParam(filterQuery.SortBy);

            if(filterQuery.CategorySearch.Any())
            {
                var categories = GetCategoriesFromFilterQuery(filterQuery.CategorySearch);
                builder.Append(categories);
            }

            if (filterQuery.SelectedFilters.Any())
            {
                var filters = GetSelectedFiltersFromFilterQuery(filterQuery.SelectedFilters);
                builder.Append(filters);
            }

            var prices = GetPricesFromFilterQuery(filterQuery.SearchPriceFilter);
            builder.Append(prices);

            //## PageNumber Params: MUST HAVE Param in the API String, ie: "&rows=24&start=0"
            builder.Append(pageNumberParams);

            //## Sort Params: Last Params in the API String, if required
            builder.Append(pageSortParams);

            return builder.ToString();
        }

        /// <summary>This will Build the Page param., with number of PageSize and PageNumber</summary>
        /// <returns>API Page param value, ie: '&rows=24&start=0'</returns>
        private string BuildUrlPagingParam(string urlParamValue)
        {
            int itemsPerPage = _appSettings.ProductsPerPage;

            if (string.IsNullOrEmpty(urlParamValue))
            {
                //## when No Page number found in the URL
                return $"&rows={itemsPerPage}&start=0";
            }

            int pageNumber = Convert.ToInt16(urlParamValue);
            int itemStart = pageNumber <= 1 ? 0 : (pageNumber - 1) * itemsPerPage;

            return $"&rows={itemsPerPage}&start={itemStart}";
        }
    }
}