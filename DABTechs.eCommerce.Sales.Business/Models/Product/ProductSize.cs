using DABTechs.eCommerce.Sales.Interfaces;

namespace DABTechs.eCommerce.Sales.Business.Models.Product
{
    public class ProductSize
    {
        public string Size { get; set; }

        public string Price { get; set; }

        public string Description { get; set; }

        public override string ToString() => string.IsNullOrEmpty(Price) ? Description : $"{Size} - {Price}";

        public string SizeAndPrice => string.IsNullOrEmpty(Price) ? Description : $"{Size} - {Price}";

        public string SizeCode { get; set; }
    }
}