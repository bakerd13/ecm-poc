using System.Collections.Generic;
using DABTechs.eCommerce.Sales.Business.Models.Product;
using DABTechs.eCommerce.Sales.Models.SearchFilters;

namespace DABTechs.eCommerce.Sales.Buisiness
{
    public class SearchResult
    {
        public List<Facet> Facets { get; set; }
        public List<Item> Items { get; set; }
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}