using DABTechs.eCommerce.Sales.Interfaces;

namespace DABTechs.eCommerce.Sales.Business.Models.Product
{
    public class Item
    {
        public string SalePrice { get; set; }
        public string ItemDescription { get; set; }
        public string OptionDescription { get; set; }
        public string SecondLine { get; set; }
        public string Brand { get; set; }
        public string Department { get; set; }
        public string ProductType { get; set; }
        public string SavingText { get; set; }
        public string SavingNumber { get; set; }
        public string Gender { get; set; }
        public string Editorial { get; set; }
        public string ProductAffiliation1 { get; set; }
        public string ProductAffiliation2 { get; set; }
        public string ProductAffiliation3 { get; set; }
        public string ProductAffiliation4 { get; set; }
        public string ProductAffiliation5 { get; set; }
        public string Colour { get; set; }

        /// <summary>
        /// Non searchable Fields
        /// </summary>
        public string Division { get; set; }

        public string ProductGroup { get; set; }
        public string ItemNo { get; set; }
        public string OptionNo { get; set; }
        public string OriginalPrice { get; set; }
        public string ForwardStock { get; set; }
        public string ReserveStock { get; set; }
        public string SupplementaryStockBalance { get; set; }
        public string TotalStockBalance { get; set; }
        public string SaleStateId { get; set; }
        public string SpecialOffer { get; set; }
        public string HasPriceHistory { get; set; }
        public string WasPrice { get; set; }

        public string[] Options { get; set; }
        public string[] SalePrices { get; set; }
        public string[] OptionDescriptions { get; set; }
        public int MinSalePrice { get; set; }
        public int MaxSalePrice { get; set; }
        public string AvgSalePrice { get; set; }
    }
}