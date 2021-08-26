using System.Collections.Generic;
using System.Linq;

namespace DABTechs.eCommerce.Sales.Business.Models.SearchFilters
{
    public class FilterGroup
    {
        public string Name { get; set; }

        public int FiltersCount
        {
            get
            {
                return FilterItems.Count();
            }
        }

        public IEnumerable<FilterItem> FilterItems { get; set; }
    }
}
