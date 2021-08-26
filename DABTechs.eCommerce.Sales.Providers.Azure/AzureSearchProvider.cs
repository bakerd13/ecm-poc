using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Options;
using DABTechs.eCommerce.Sales.Common.Config;
using DABTechs.eCommerce.Sales.Common.Enums;
using DABTechs.eCommerce.Sales.Business.Collections;
using DABTechs.eCommerce.Sales.Business.Interfaces;
using DABTechs.eCommerce.Sales.Providers.Azure.Mapper;
using DABTechs.eCommerce.Sales.Business;
using DABTechs.eCommerce.Sales.Models.Search;

namespace DABTechs.eCommerce.Sales.Providers.Azure
{
    public class AzureSearchProvider : ISearchProvider
    {
        private static readonly Regex brandBadCharsRegex = new Regex(@"[_&':‘’“”.!\-]", RegexOptions.Compiled);
        private static readonly Regex itemNumberRegex = new Regex(@"([a-zA-Z\d]{3}-[a-zA-Z\d]{3})$", RegexOptions.Compiled);
        private static readonly Regex priceRangeRegex = new Regex(@"price:\[(\d*),(\d*)\]", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex underscoresRegex = new Regex(@"[_]", RegexOptions.Compiled);
        private static readonly Regex underscoresSpacesRegex = new Regex(@"[ _]", RegexOptions.Compiled);
        private readonly AppSettings _appSettings;
        private readonly AzureHttpClient _azureSearch;

        public AzureSearchProvider(AzureHttpClient client, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _azureSearch = client;
        }

        public SearchProvider SearchProviderType { get => SearchProvider.Azure; }

        /// <summary>
        /// Gets the raw result string from the search provider.
        /// </summary>
        /// <param name="searchQuery">The search query.</param>
        /// <returns></returns>
        public async Task<string> GetApiResult(FilterQuery filterQuery)
        {
            try
            {
                string azureAPI = BuildQueryString(filterQuery);
                string rawData = await _azureSearch.Client.GetStringAsync(azureAPI).ConfigureAwait(false);

                return rawData;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("ERR_FAILED_TO_GET_AZURE_SEARCH", ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Map the raw result to the SearchResultCollection.
        /// </summary>
        /// <param name="rawResult">The raw result.</param>
        /// <returns></returns>
        public SearchResults Map(string rawResult)
        {
            try
            {
                AzureSearchResultMapper mapper = new AzureSearchResultMapper(_appSettings);
                var result = mapper.Map(rawResult);
                return result;
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Failed to map azure search result: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Applies the multi filter.
        /// </summary>
        /// <param name="sb">The sb.</param>
        /// <param name="name">The name.</param>
        /// <param name="values">The values.</param>
        private static void ApplyMultiFilter(StringBuilder sb, string name, ICollection<string> values, bool removeSpaces = false)
        {
            if (values == null || !values.Any()) return;

            var formattedValues = values.Select(x => removeSpaces ? HttpUtility.UrlEncode(underscoresSpacesRegex.Replace(x, string.Empty).ToLower()) : HttpUtility.UrlEncode(underscoresRegex.Replace(x, " ")));
            var filterString = string.Join("||", formattedValues);
            sb.Append($"{(sb.Length > 0 ? " AND " : "")}{name}[{filterString}]");
        }

        /// <summary>
        /// Applies the filter.
        /// </summary>
        /// <param name="sb">The sb.</param>
        /// <param name="name">The name.</param>
        /// <param name="values">The values.</param>
        private StringBuilder ApplySelectedFilters(List<AfValue> afValues, bool removeSpaces = false)
        {
            StringBuilder af = new StringBuilder();

            if (afValues == null || !afValues.Any()) return af;

            var values = afValues.Select(x => x.Name).Distinct();

            foreach (var name in afValues.Select(x => x.Name).Distinct())
            {
                var formattedValues = afValues.Where(w => w.Name == name).Select(x => x.Value = removeSpaces ? HttpUtility.UrlEncode(underscoresSpacesRegex.Replace(x.Value, string.Empty).ToLower()) : HttpUtility.UrlEncode(underscoresRegex.Replace(x.Value, " ")));
                var filterString = string.Join("||", formattedValues);
                af.Append($"{(af.Length > 0 ? " AND " : "")}{name}[{filterString}]");
            }

            return af;
        }

        /// <summary>
        /// Extracts the price range.
        /// </summary>
        /// <param name="af">The af.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns></returns>
        private static bool TryExtractPriceRange(string af, out int minValue, out int maxValue)
        {
            minValue = 0;
            maxValue = 0;

            var match = priceRangeRegex.Match(af ?? "");
            if (match.Success && match.Groups.Count == 3)
            {
                minValue = int.Parse(match.Groups[1].Value) / 100;
                maxValue = int.Parse(match.Groups[2].Value) / 100;

                return true;
            }

            return false;
        }

        /// <summary>Build the Query Search String- transforming the Generic URL of our website</summary>
        /// <returns>Provider Specific Query String for API Call</returns>
        private string BuildQueryString(FilterQuery filterQuery)
        {
            // TODO Refactor further
            StringBuilder sb = new StringBuilder();

            sb.Append("?profile=VIP");

            StringBuilder af = ApplySelectedFilters(filterQuery.SelectedFilters);
            
            // TODO Check Price Range from frontend if there is any logic required
            var priceRange = $"avgSalePrice({filterQuery.SearchPriceFilter.PriceFrom},{filterQuery.SearchPriceFilter.PriceTo}) ";

            // TODO Brand
            //ApplyMultiFilter(af, "brandCode", searchQuery?.BrandValues?.Select(x => brandBadCharsRegex.Replace(x, string.Empty)).ToList(), true);

            // TODO Discount must be managed from frontend
            //if (searchQuery.SavingsValues != null && searchQuery.SavingsValues.Contains("over60") && !searchQuery.SavingsValues.Contains("over70"))
            //{
            //    ApplyMultiFilter(af, "discount", new[] { "over60", "over70" });
            //}
            //else
            //{
            //    ApplyMultiFilter(af, "discount", searchQuery.SavingsValues);
            //}

            // Is keyword Serach Or Category Search
            if (!string.IsNullOrWhiteSpace(filterQuery.KeywordSearch) && filterQuery.KeywordSearch != "*")
            {
                // If the query is an item number with a hypen in, remove all hyphens from the string
                if (itemNumberRegex.IsMatch(filterQuery.KeywordSearch))
                {
                    filterQuery.KeywordSearch = filterQuery.KeywordSearch.Replace("-", "");
                }

                sb.Append("&w=" + filterQuery.KeywordSearch.Replace(" ", "%20"));
                
                if (af.Length > 0)
                {
                    sb.Append($"&af={priceRange}AND " + af.ToString());
                }
                else
                {
                    sb.Append($"&af={priceRange}" + af.ToString());
                }
            }
            else
            {
                StringBuilder categories = ApplySelectedFilters(filterQuery.CategorySearch);
                af.Append(categories);
                sb.Append($"&af={priceRange}" + af.ToString());
            }           

            // Paging
            if (filterQuery.PageNumber > 0)
            {
                sb.Append("&srt=" + (filterQuery.PageNumber - 1) * 24);
            }

            // Sort Order
            var sortOrder = "&isort=orderValue desc";
            if (!string.IsNullOrWhiteSpace(filterQuery.SortBy))
            {
                if (filterQuery.SortBy == "price")
                {
                    sortOrder = "&isort=minSalePrice asc";
                }
                else if (filterQuery.SortBy == "price rev" || filterQuery.SortBy == "price%20rev")
                {
                    sortOrder = "&isort=minSalePrice desc";
                }
            }
            sb.Append(sortOrder);
        
            return sb.ToString().Trim();
        }
    }
}