using DABTechs.eCommerce.Sales.Interfaces;

namespace DABTechs.eCommerce.Sales.Models.Search
{
    public class AfValue
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}