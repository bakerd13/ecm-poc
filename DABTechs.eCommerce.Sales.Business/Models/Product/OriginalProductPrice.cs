namespace DABTechs.eCommerce.Sales.Business.Models.Product
{
    public class OriginalProductPrice : ProductPriceBase
    {
        public override string ToString() => GetPriceRange();
    }
}