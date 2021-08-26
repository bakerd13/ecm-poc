namespace DABTechs.eCommerce.Sales.Business.Models.Product
{
    public abstract class ProductPriceBase
    {
        public double MinSalePrice { get; set; }

        public double MaxSalePrice { get; set; }

        private string MinSalePriceMoney { get { return (MinSalePrice).ToString("C2"); } }
        private string MaxSalePriceMoney { get { return (MaxSalePrice).ToString("C2"); } }

        protected string GetPriceRange() => MinSalePrice != MaxSalePrice ? $"{MinSalePriceMoney} - {MaxSalePriceMoney}" : MinSalePriceMoney;
    }
}