using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using DABTechs.eCommerce.Sales.Common;
using DABTechs.eCommerce.Sales.Common.Config;
using DABTechs.eCommerce.Sales.Business.Collections;
using DABTechs.eCommerce.Sales.Business.Models.Product;
using DABTechs.eCommerce.Sales.Business.Models.SearchCategories;
using DABTechs.eCommerce.Sales.Providers.BloomReach.Models;
using DABTechs.eCommerce.Sales.Business;
using DABTechs.eCommerce.Sales.Business.Interfaces;
using DABTechs.eCommerce.Sales.Business.Models.SearchFilters;

namespace DABTechs.eCommerce.Sales.Providers.BloomReach.Mapper
{
    public class BloomReachSearchResultMapper : SearchResultsMapperBase, ISearchResultMapper
    {
        private readonly string[] FacetsThatNeedSpaces = { "next_category" };
        readonly string[] FacetsThatDontNeedSanitisation = { "sizes" };

        public BloomReachSearchResultMapper(AppSettings appSettings) : base(appSettings)
        {
        }

        /// <summary>
        /// Maps the specified raw result to a SearchResultCollection.
        /// </summary>
        /// <param name="rawResult">The raw result.</param>
        /// <returns></returns>
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
                BloomReachModel bloomReachModel = ConvertToModel(rawResult);

                // Map the facet filters.
                searchResults.ProductFilters = MapFilters(bloomReachModel);

                // Map the product items.
                searchResults.ProductItems = MapProducts(bloomReachModel);
               
                // Pagination.
                searchResults.TotalMatchingResults = bloomReachModel.Response.TotalResults;

                // Return the results.
                return searchResults;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to map bloomreach search results");
            }

            return searchResults;
        }

        /// <summary>
        /// Maps the facets.
        /// </summary>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="searchResult">The search result.</param>
        /// <returns></returns>
        private CategoriesCollection MapFacets(BloomReachModel searchResult)
        {
            try
            {
                var brandFilterItems = MapToFacetGroup(searchResult?.Facets?.FacetFields?.Brands, "brands");
                var colourFilterItems = MapToFacetGroup(searchResult?.Facets?.FacetFields?.Colours, "colour");
                var departmentFilterItems = MapToFacetGroup(searchResult?.Facets?.FacetFields?.Departments, "department");
                var productCategoryFilterItems = MapToFacetGroup(searchResult?.Facets?.FacetFields?.Categories, "next_category");
                var savingFilterItems = MapToFacetGroup(searchResult?.Facets?.FacetFields?.Discount, "discount", "", "");
                var sizeOptionFilterItems = MapToFacetGroup(searchResult?.Facets?.FacetFields?.Sizes, "sizes");
                var priceRangeFilter = MapPriceRange(searchResult);
                var totalSearchResult = searchResult?.Response?.TotalResults ?? 0;

                return new CategoriesCollection
                {
                    BrandFilterItems = brandFilterItems,
                    ColourFilterItems = colourFilterItems,
                    DepartmentFilterItems = departmentFilterItems,
                    ProductCategoryFilterItems = productCategoryFilterItems,
                    SavingFilterItems = savingFilterItems,
                    SizeOptionFilterItems = sizeOptionFilterItems,
                    PriceRangeFilter = priceRangeFilter,
                    TotalSearchResult = totalSearchResult
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Maps the facets.
        /// </summary>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="searchResult">The search result.</param>
        /// <returns></returns>
        private FiltersCollection MapFilters(BloomReachModel searchResult)
        { 
            try
            {
                var filters = new List<FilterGroup>();

                filters.Add(MapToFilterGroup(searchResult?.Facets?.FacetFields?.Brands, "brands", "Brands"));
                filters.Add(MapToFilterGroup(searchResult?.Facets?.FacetFields?.Colours, "colour", "Colour"));
                filters.Add(MapToFilterGroup(searchResult?.Facets?.FacetFields?.Departments, "department", "Department"));
                filters.Add(MapToFilterGroup(searchResult?.Facets?.FacetFields?.Categories, "next_category", "Category"));
                filters.Add(MapToFilterGroup(searchResult?.Facets?.FacetFields?.Discount, "discount", "Discount", "", ""));
                filters.Add(MapToFilterGroup(searchResult?.Facets?.FacetFields?.Sizes, "sizes", "Sizes"));
                
                
                var priceRangeFilter = MapPriceRange(searchResult);
                var totalSearchResult = searchResult?.Response?.TotalResults ?? 0;

                return new FiltersCollection
                {
                    Filters = filters,
                    PriceRangeFilter = priceRangeFilter,
                    TotalSearchResult = totalSearchResult
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Maps the products.
        /// </summary>
        /// <param name="searchResult">The search result.</param>
        /// <returns></returns>
        private List<ProductItemCollection> MapProducts(BloomReachModel searchResult)
        {
            ICollection<BloomReachItem> items = searchResult.Response.Items;

            var productItems = new List<ProductItemCollection>(items.Count);

            try
            {
                foreach (BloomReachItem item in items)
                {
                    double minPrice = item.GiveMinSalePrice();
                    double maxPrice = item.GiveMaxSalePrice();
                    double minOrginalPrice = item.GiveMinOriginalPrice();
                    double maxOrginalPrice = item.GiveMaxOriginalPrice();

                    ProductItemCollection productItem = new ProductItemCollection
                    {
                        Description = item.Title,
                        ItemNo = item.PID.Substring(0, 6),
                        ImageUrl = base.GiveProductImageUrl(item.PID),
                        Discount = item.GiveDiscount(),
                        SizeDescriptions = item.GiveSizeDescriptions(),
                        SizeCodes = item.GiveVariantSizes(),
                        SizeOptions = item.GiveSizeOptions(),
                        SalePrices = item.GiveSalePrices(),
                        Sizes = item.GiveSizeDescriptions(),
                        Title = item.Title,
                        Composition = item.Description,
                        CurrentPrice = new CurrentProductPrice(minPrice, maxPrice),
                        OriginalPrice = new OriginalProductPrice() { MinSalePrice = minOrginalPrice, MaxSalePrice = maxOrginalPrice }
                    };

                    productItem.PriceHistory = PriceHistoryListMapper(item.GivePriceHistory());

                    if (productItem.CurrentPrice.MinSalePrice != productItem.CurrentPrice.MaxSalePrice)
                    {
                        productItem.SizeAndPriceList = BuildSizeAndPriceListItems(productItem);
                    }
                    else
                    {
                        productItem.SizeAndPriceList = BuildSizeAndPriceListItems(productItem, false);
                    }

                    productItems.Add(productItem);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString() + ex.StackTrace.ToString());
                throw;
            }

            return productItems;
        }

        /// <summary>
        /// Builds the size and price list items.
        /// </summary>
        /// <param name="productItem">The product item.</param>
        /// <param name="appendPrice">if set to <c>true</c> [append price].</param>
        /// <returns></returns>
        private static List<ProductSize> BuildSizeAndPriceListItems(ProductItemCollection productItem, bool appendPrice = true)
        {
            List<ProductSize> result = new List<ProductSize>();

            for (int x = 0; x < productItem.SizeDescriptions.Count; x++)
            {
                result.Add(new ProductSize()
                {
                    Description = productItem.SizeDescriptions[x],
                    Price = appendPrice ? Convert.ToDecimal(productItem.SalePrices[x]).ToString("C") : null,
                    Size = productItem.SizeCodes[x],
                    SizeCode = productItem.SizeCodes[x]
                });
            }

            result = result.OrderBy(r => r.SizeCode, new SalePriceComparer()).ToList();
            result.Insert(0, (new ProductSize() { Description = "Size" }));

            return result;
        }

        /// <summary>This will Convert a BR JSON FacetGroup to a Generic Facet Group- suitable for SLI, Azure, BR</summary>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        private IEnumerable<FilterItem> MapToFacetGroup(IList<FacetElementBR> brFacetList, string facetGroupName, string preTitle = null, string postTitle = null)
        {
            if (brFacetList == null)
            {
                return new List<FilterItem>();
            }

            List<FilterItem> result = new List<FilterItem>();

            try
            {
                string itemName = "";
                foreach (var item in brFacetList)
                {
                    itemName = item.Name.Replace(" ", "_"); //## We replaced 'empty space' with 'underscore' to keep the URL clean. Now put the space back to check similarity check to see whether it exist
                    if (item.Name.StartsWith("mn-")) itemName = item.Name.Replace("mn-", ""); //## MegaNav menu items are prefixed with "mn-", ie: "mn-sweatshirts", "mn-shoes". Remove that "mn-" to match facets and set checked true/false

                    string queryString = "";

                    var filterItem = new FilterItem()
                    {
                        FacetName = facetGroupName,
                        Title = $"{preTitle}{item.Name}{postTitle}",
                        Value = item.Name.Sanitize(FacetsThatNeedSpaces.Contains(facetGroupName)),
                        Selected = UrlHelper.ParamExist(queryString, facetGroupName, itemName),
                        Count = item.Count
                    };

                    // Do not display 0 value discounts.
                    if (filterItem.FacetName == "discount" && (string.IsNullOrWhiteSpace(filterItem.Value) || filterItem.Value == "0"))
                    {
                        continue;
                    }

                    result.Add(filterItem);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString() + ex.StackTrace.ToString());
                throw;
            }

            return result.AsEnumerable();
        }

        /// <summary>This will Convert a BR JSON FacetGroup to a Generic Facet Group- suitable for SLI, Azure, BR</summary>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        private FilterGroup MapToFilterGroup(IList<FacetElementBR> brFacetList, string facetGroupName, string displayName, string preTitle = null, string postTitle = null)
        {
            if (brFacetList == null)
            {
                return new FilterGroup
                {
                    Name = displayName,
                    FilterItems = new List<FilterItem>(),
                };
            }

            List<FilterItem> result = new List<FilterItem>();

            try
            {
                string itemName = "";
                foreach (var item in brFacetList)
                {
                    itemName = item.Name.Replace(" ", "_"); //## We replaced 'empty space' with 'underscore' to keep the URL clean. Now put the space back to check similarity check to see whether it exist
                    if (item.Name.StartsWith("mn-")) itemName = item.Name.Replace("mn-", ""); //## MegaNav menu items are prefixed with "mn-", ie: "mn-sweatshirts", "mn-shoes". Remove that "mn-" to match facets and set checked true/false

                    string queryString = "";

                    var filterItem = new FilterItem()
                    {
                        FacetName = facetGroupName,
                        Title = $"{preTitle}{item.Name}{postTitle}",
                        Value = item.Name.Sanitize(FacetsThatNeedSpaces.Contains(facetGroupName)),
                        Selected = UrlHelper.ParamExist(queryString, facetGroupName, itemName),
                        Count = item.Count
                    };

                    // Do not display 0 value discounts.
                    if (filterItem.FacetName == "discount" && (string.IsNullOrWhiteSpace(filterItem.Value) || filterItem.Value == "0"))
                    {
                        continue;
                    }

                    result.Add(filterItem);
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

        private BloomReachModel ConvertToModel(string jsonResult)
        {
            BloomReachModel result = JsonConvert.DeserializeObject<BloomReachModel>(jsonResult);
            return result;
        }

        /// <summary>Returns an Object with Min and Max Price from the Search Result object to be used in PriceSlider</summary>
        /// <param name="queryString">Query String on the URL</param>
        /// <returns>PriceRangeFilter Object</returns>
        private static PriceRangeFilter MapPriceRange(BloomReachModel searchResult)
        {
            PriceRangeFilter result = null;

            try
            {
                double minPrice = searchResult?.Stats?.StatFields?.SalePrices?.MinimumMarkPrice ?? 0;
                double maxPrice = searchResult?.Stats?.StatFields?.SalePrices?.MaximumMarkPrice ?? 0;
                result = new PriceRangeFilter { Min = minPrice, Max = maxPrice };
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString() + ex.StackTrace.ToString());
            }

            return result;
        }

        /// <summary>This will Map the BloomReach Price History to Generic Price History Model</summary>
        /// <param name="jsonPriceList">String array of Price History</param>
        /// <returns>List of Price items</returns>
        private static List<PriceList> PriceHistoryListMapper(string[] jsonPriceList)
        {
            if (jsonPriceList == null || jsonPriceList.All(x => string.IsNullOrWhiteSpace(x)))
            {
                return new List<PriceList>();
            }

            var results = new List<PriceList>();

            var grouping = jsonPriceList.GroupBy(x => x.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)[1].Remove(7));

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
    }
}