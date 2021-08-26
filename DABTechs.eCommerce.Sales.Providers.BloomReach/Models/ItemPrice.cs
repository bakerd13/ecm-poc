using DABTechs.eCommerce.Sales.Business.Models.Product;

namespace DABTechs.eCommerce.Sales.Providers.BloomReach.Models
{
    public class ItemPrice
    {
        public OriginalProductPrice OriginalProductPrice { get; set; }
        public CurrentProductPrice CurrentProductPrice { get; set; }
    }
}