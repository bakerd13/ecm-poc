using DABTechs.eCommerce.Sales.Providers.Azure.Interfaces;

namespace DABTechs.eCommerce.Sales.Providers.Azure.Models
{
    public class FacetElement : IFacetElement
    {
        public string Value { get; set; }

        public int Count { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}