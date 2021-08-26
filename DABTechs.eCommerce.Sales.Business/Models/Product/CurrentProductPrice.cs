namespace DABTechs.eCommerce.Sales.Business.Models.Product
{
    public class CurrentProductPrice : ProductPriceBase
    {
        /// <summary>Product Price may come with range- a PID can have 5 items with various price</summary>
        /// <param name="min">Minimum price in the Size of that Product</param>
        /// <param name="max">Maximum price for the large size</param>
        public CurrentProductPrice(double min, double max)
        {
            MinSalePrice = min;
            MaxSalePrice = max;
        }

        public override string ToString() => GetPriceRange();
    }
}