using System.Collections.Generic;

namespace DABTechs.eCommerce.Sales.Business.Collections
{
    /// <summary>
    /// The Search Result Collection.
    /// </summary>
    public class SearchResults
    {
        public ICollection<ProductItemCollection> ProductItems { get; set; }

        public FiltersCollection ProductFilters { get; set; }

        public int TotalMatchingResults { get; set; }

        public string LastUpdatedDateTimeStamp { get; set; }

        public FilterQuery FilterQuery { get; set; }
    }
}