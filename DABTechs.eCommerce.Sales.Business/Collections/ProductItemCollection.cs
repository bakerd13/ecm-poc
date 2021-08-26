using System.Collections.Generic;
using System.Linq;
using System.Text;
using DABTechs.eCommerce.Sales.Business.Models.Product;

namespace DABTechs.eCommerce.Sales.Business.Collections
{
    public class ProductItemCollection
    {
        public string ItemNo { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ProductCode => ItemNo.Length > 3 ? ItemNo.Insert(3, "-") : ItemNo;

        public string ImageUrl { get; set; }

        public OriginalProductPrice OriginalPrice { get; set; }

        public CurrentProductPrice CurrentPrice { get; set; }

        public string Discount
        {
            get;
            set;
        }

        public string DiscountClass
        {
            get
            {
                if (Discount == "over70") { return "percent70"; }
                if (Discount == "over60") { return "percent60"; }
                return string.Empty;
            }
        }

        public bool HasPriceHistory
        {
            get
            {
                return (PriceHistory != null && PriceHistory.Count > 0);
            }
        }

        public IList<PriceList> PriceHistory { get; set; }

        public IList<string> Sizes { get; set; }

        public IList<string> SalePrices { get; set; }

        public IList<string> SizeDescriptions { get; set; }

        /// <summary>This will provide a list of Sizes with respective prices, for Dropdown list</summary>
        /// <returns>List of ProductSize object</returns>
        public IList<ProductSize> SizeAndPriceList { get; set; }

        public string Composition { get; set; }
        public IList<string> SizeCodes { get; set; }
        public IList<string> SizeOptions { get; set; }

        /// <summary>To be used for 'Sizes still available:' - Concatenated list of All sizes, ie: 10, 12, 16</summary>
        /// <returns>List of sizes, separated by comma</returns>
        public string GetAvailableSizes()
        {
            // Ensure we have some sizes.
            if (SizeAndPriceList == null || SizeAndPriceList.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder sizeList = new StringBuilder();
            foreach (var item in SizeAndPriceList.Where(x => x.Description != "Size"))
            {
                if (sizeList.Length > 0)
                {
                    sizeList.Append(", ");
                }
                sizeList.Append(item.Description);
            }

            return sizeList.ToString();
        }

        public override string ToString()
        {
            return $"{ItemNo} - {Title}";
        }
    }
}