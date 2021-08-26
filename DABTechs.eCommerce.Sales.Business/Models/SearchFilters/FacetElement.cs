using DABTechs.eCommerce.Sales.Interfaces;

namespace DABTechs.eCommerce.Sales.Models.SearchFilters
{
    public class FacetElement
    {
        public string Value { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}