using DABTechs.eCommerce.Sales.Business.Models.Search;
using DABTechs.eCommerce.Sales.Models.Search;
using System;
using System.Collections.Generic;
using System.Text;

namespace DABTechs.eCommerce.Sales.Business
{
    public class FilterQuery
    {
        public string KeywordSearch { get; set; }

        public List<AfValue> CategorySearch { get; set; }

        public List<AfValue> SelectedFilters { get; set; }

        public PriceValue SearchPriceFilter { get; set; }

        public int PageNumber { get; set; }

        public string SortBy { get; set; } = "bst";
    }
}
