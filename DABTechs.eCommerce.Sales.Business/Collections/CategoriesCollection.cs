using System.Collections.Generic;
using DABTechs.eCommerce.Sales.Business.Models.SearchCategories;
using DABTechs.eCommerce.Sales.Business.Models.SearchFilters;

namespace DABTechs.eCommerce.Sales.Business.Collections
{
    public class CategoriesCollection
    {
        public IEnumerable<FilterItem> ProductCategoryFilterItems { get; set; }

        public IEnumerable<FilterItem> DepartmentFilterItems { get; set; }

        /// <summary>Discount values</summary>
        public IEnumerable<FilterItem> SavingFilterItems { get; set; }

        public IEnumerable<FilterItem> BrandFilterItems { get; set; }

        public IEnumerable<FilterItem> ColourFilterItems { get; set; }

        public IEnumerable<FilterItem> SizeOptionFilterItems { get; set; }

        /// <summary>This will return the Price range Min-Max, as this CategoriesMenuModel object will be passed to create Facets-
        /// it will be easier to read Price Range from this Model, rather from the Response Model</summary>
        public PriceRangeFilter PriceRangeFilter { get; set; }

        public int TotalSearchResult { get; set; }

        public string GetProductsCount() => string.Concat(TotalSearchResult, " Products");
    }
}