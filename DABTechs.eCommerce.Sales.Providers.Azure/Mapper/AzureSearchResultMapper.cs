using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using DABTechs.eCommerce.Sales.Common;
using DABTechs.eCommerce.Sales.Common.Config;
using DABTechs.eCommerce.Sales.Business.Collections;
using DABTechs.eCommerce.Sales.Business.Models.Product;
using DABTechs.eCommerce.Sales.Business.Models.SearchCategories;
using DABTechs.eCommerce.Sales.Providers.Azure.Models;
using Facet = DABTechs.eCommerce.Sales.Providers.Azure.Models.Facet;
using FacetElement = DABTechs.eCommerce.Sales.Providers.Azure.Models.FacetElement;
using Item = DABTechs.eCommerce.Sales.Providers.Azure.Models.Item;
using DABTechs.eCommerce.Sales.Business;
using DABTechs.eCommerce.Sales.Business.Interfaces;
using DABTechs.eCommerce.Sales.Business.Models.SearchFilters;

namespace DABTechs.eCommerce.Sales.Providers.Azure.Mapper
{
    /// <summary>
    /// The Azure Search Result Mapper.
    /// </summary>
    /// <seealso cref="ISearchResultMapper" />
    public class AzureSearchResultMapper : SearchResultsMapperBase, ISearchResultMapper
    {
        public AzureSearchResultMapper(AppSettings appSettings) : base(appSettings)
        {
        }

        public override SearchResults Map(string rawResult)
        {
            SearchResults searchResults = new SearchResults();

            try
            {
                // If the rawResult is empty, return a default PaginationCollection.
                if (string.IsNullOrWhiteSpace(rawResult))
                {
                    return searchResults;
                }

                // Convert to to the bloomreach model.
                AzureSearchResults searchResultAzure = ConvertToModel(rawResult);

                // Map Facets
                searchResults.ProductFilters = MapFilters(searchResultAzure);

                // Map Products
                searchResults.ProductItems = MapProducts(searchResultAzure);
               
                searchResults.TotalMatchingResults = searchResultAzure.TotalResults;

                // Return the results.
                return searchResults;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to map azure search results");
            }

            return searchResults;
        }

        /// <summary>This will Transform the Raw data provided from Provider into a Generic Data model which can be rendered on View Page without knowing who is the Provider</summary>
        /// <param name="jsonResult">Raw API Result</param>
        /// <returns>SearchResult Model</returns>
        private AzureSearchResults ConvertToModel(string jsonResult)
        {
            try
            {
                var model = JsonConvert.DeserializeObject<AzureSearchResults>(jsonResult);
                return model;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Maps the product items.
        /// </summary>
        /// <param name="searchResultAzure">The search result azure.</param>
        /// <returns></returns>
        private ICollection<ProductItemCollection> MapProducts(AzureSearchResults searchResultAzure)
        {
            List<Item> items = searchResultAzure.Items;

            var productItems = new List<ProductItemCollection>();

            var baseImageUri = new Uri(_appSettings.ImageBaseUrl);

            try
            {
                foreach (Item item in items)
                {
                    double minPrice = Convert.ToDouble(item["minSalePrice"]?.Value);
                    double maxPrice = Convert.ToDouble(item["maxSalePrice"]?.Value);
                    double minOrginalPrice = Convert.ToDouble(item["minOriginalPrice"]?.Value);
                    double maxOrginalPrice = Convert.ToDouble(item["maxOriginalPrice"]?.Value);

                    ProductItemCollection productItem = new ProductItemCollection
                    {
                        Description = item["itemDescription"]?.Value,
                        ItemNo = item["itemNo"]?.Value.Substring(0, 6),
                        ImageUrl = new Uri(baseImageUri, GetImageUrl(item["itemNo"]?.Value)).AbsoluteUri,
                        Discount = item["discount"]?.Value,
                        SizeDescriptions = item["optionDescriptions"]?.Options,
                        SalePrices = item["salePrices"]?.Options,
                        Sizes = item["optionDescriptions"]?.Options,
                        Title = item["itemDescription"]?.Value,
                        Composition = item["composition"]?.Value,
                        CurrentPrice = new CurrentProductPrice(minPrice, maxPrice),
                        OriginalPrice = new OriginalProductPrice() { MinSalePrice = minOrginalPrice, MaxSalePrice = maxOrginalPrice }
                    };

                    if (item.HasValue("priceHistory"))
                    {
                        productItem.PriceHistory = PriceHistoryListMapper(item["priceHistory"].Options);
                    }

                    if (productItem.CurrentPrice.MinSalePrice != productItem.CurrentPrice.MaxSalePrice)
                    {
                        productItem.SizeAndPriceList = BuildSizeAndPriceListItems(item);
                    }
                    else
                    {
                        productItem.SizeAndPriceList = BuildSizeAndPriceListItems(item, false);
                    }

                    productItems.Add(productItem);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Failed to map azure item.", ex.Message);
                throw;
            }

            return productItems;
        }

        private static List<ProductSize> BuildSizeAndPriceListItems(Item item, bool appendPrice = true)
        {
            List<ProductSize> result = new List<ProductSize>();

            for (int idx = 0; idx < item["options"].Count; idx++)
            {
                ProductSize productSize = new ProductSize()
                {
                    Size = item["optionCodes"][idx],
                    Price = appendPrice ? Convert.ToDecimal(item["salePrices"][idx]).ToString("C") : null,
                    Description = item["optionDescriptions"][idx],
                    SizeCode = item["options"][idx],
                };

                result.Add(productSize);
            }

            result = result.OrderBy(r => r.SizeCode, new SalePriceComparer()).ToList();
            result.Insert(0, (new ProductSize() { Description = "Size" }));

            return result;
        }

        private FiltersCollection MapFilters(AzureSearchResults searchResultAzure)
        {
            try
            {
                var filters = new List<FilterGroup>();

                filters.Add(MapToFilterGroup(searchResultAzure["brand"], "brands", "Brands"));
                filters.Add(MapToFilterGroup(searchResultAzure["colour"], "colour", "Colour"));
                filters.Add(MapToFilterGroup(searchResultAzure["gender"], "department", "Department"));
                filters.Add(MapToFilterGroup(searchResultAzure["category"], "next_category", "Category"));
                filters.Add(MapToFilterGroup(searchResultAzure["optionFacets"], "size", "Size"));
                filters.Add(MapToFilterGroup(searchResultAzure["discount"], "discount", "Discount"));

                if (searchResultAzure.Items.Count == 0) { searchResultAzure.TotalResults = 0; }

                double minPrice = searchResultAzure["minSalePrice"].MinFacetValue();
                double maxPrice = searchResultAzure["maxSalePrice"].MaxFacetValue();

                var priceRangeFilter = new PriceRangeFilter();

                if (minPrice > 0 && maxPrice > 0)
                {
                    priceRangeFilter = new PriceRangeFilter() { Min = minPrice, Max = maxPrice };
                }

                return new FiltersCollection
                {
                    Filters = filters,
                    PriceRangeFilter = priceRangeFilter,
                    TotalSearchResult = searchResultAzure.TotalResults,
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex); throw;
            }
        }

        private FilterGroup MapToFilterGroup(Facet facet, string facetGroupName, string displayName)
        {
            List<FilterItem> result = new List<FilterItem>();

            try
            {
                if (facet == null) {
                    return new FilterGroup
                    {
                        Name = displayName,
                        FilterItems = new List<FilterItem>(),
                    };
                }

                foreach (FacetElement item in facet.Elements)
                {
                    string[] optionFacets = item.Value.Split('|');
                    string optionFacetValue = optionFacets[0];
                    string optionFacetTitle = (optionFacets.Length > 1) ? optionFacets[1] : optionFacets[0];

                    if (optionFacets.Length > 1)
                    {
                        optionFacetTitle = optionFacets[1];
                    }

                    var filterItem = new FilterItem()
                    {
                        FacetName = $"{facetGroupName}_{optionFacetValue.Replace(" ", "")}",
                        Title = SearchQueryManager.Map(facetGroupName, optionFacetTitle),
                        Value = optionFacetValue.Replace(" ", "_"),
                        Count = item.Count,
                    };

                    // Do not display 0 value discounts.
                    if (filterItem.FacetName.Equals("discount", StringComparison.OrdinalIgnoreCase))
                    {
                        if (filterItem.Title.Equals("over60", StringComparison.OrdinalIgnoreCase))
                        {
                            filterItem.Title = "Over 60%";
                        }
                        else if (filterItem.Title.Equals("over70", StringComparison.OrdinalIgnoreCase))
                        {
                            filterItem.Title = "Over 70%";
                        }

                        if (string.IsNullOrWhiteSpace(filterItem.Value) || filterItem.Value == "0" || filterItem.Title.Equals("any", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                    }
                    else if (filterItem.FacetName.Equals("category", StringComparison.OrdinalIgnoreCase))
                    {
                        filterItem.Value = HttpUtility.UrlEncode(filterItem.Value);
                    }


                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString() + ex.StackTrace.ToString());
                throw;
            }

            return new FilterGroup
            {
                Name = displayName,
                FilterItems = result.AsEnumerable(),
            };
        }

        private static List<List<ProductItemCollection>> GroupProductItems(ICollection<ProductItemCollection> productItems)
        {
            var groups = new List<List<ProductItemCollection>>();

            var productItemCollections = productItems.ToList();

            var groupsCount = productItemCollections.Count / 2;

            if (productItemCollections.Count % 2 > 0)
                groupsCount++;

            for (var i = 0; i < groupsCount; i++)
                groups.Add(productItemCollections.Skip(i * 2).Take(2).ToList());

            return groups;
        }

        /// <summary>This will Map the BloomReach Price History to Generic Price History Model</summary>
        /// <param name="jsonPriceList">String array of Price History</param>
        /// <returns>List of Price items</returns>
        private static List<PriceList> PriceHistoryListMapper(ICollection<string> jsonPriceList)
        {
            var results = new List<PriceList>();

            var priceHistories = new List<string>();
            foreach (var jsonPrice in jsonPriceList)
            {
                priceHistories.AddRange(jsonPrice.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries));
            }

            var grouping = priceHistories.GroupBy(x => x.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)[1].Remove(7));

            foreach (var _group in grouping)
            {
                var lowest = _group.Select(x => double.Parse(x.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)[0])).Min();
                var highest = _group.Select(x => double.Parse(x.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)[0])).Max();

                var range = lowest == highest ? $"£{lowest:0.00}" : $"£{lowest:0.00} - £{highest:0.00}";

                results.Add(new PriceList { Date = (DateTime)Dates.FormatToDate(_group.Key), Price = range });
            }

            results = results.OrderBy(x => x.Date).ToList();

            return results;
        }

        private static string GetImageUrl(string pid)
        {
            if (string.IsNullOrWhiteSpace(pid)) return string.Empty;

            var formattedPid = pid.Length > 6 ? $"{pid.Insert(6, "-")}" : pid;

            return $"{formattedPid}.jpg";
        }
    }
}