using System.Collections.Generic;
using System.Text;
using DABTechs.eCommerce.Sales.Business.Models.Product;

namespace DABTechs.eCommerce.Sales.Models
{
    public class ProductItemModel
    {
        public string ItemNo { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ProductCode => ItemNo.Length > 3 ? ItemNo.Insert(3, "-") : ItemNo;

        public string ImageUrl { get; set; }

        public OriginalProductPrice OriginalPrice { get; set; }

        public CurrentProductPrice CurrentPrice { get; set; }

        public string Discount { get; set; }

        public List<PriceList> PriceHistory { get; set; }
        public List<string> Sizes { get; set; }

        public IReadOnlyList<string> SalePrices { get; set; }

        public List<string> SizeDescriptions { get; set; }

        /// <summary>This will provide a list of Sizes with respective prices, for Dropdown list</summary>
        /// <returns>List of ProductSize object</returns>
        public IEnumerable<ProductSize> SizeAndPriceList { get; set; }

        /// <summary>To be used for 'Sizes still available:' - Concatenated list of All sizes, ie: 10, 12, 16</summary>
        /// <returns>List of sizes, separated by comma</returns>
        public string GetAvailableSizes()
        {
            if (Sizes == null) return "";                                                //## Nothing in the Size variant- return Empty
            if (Sizes.Count >= 1 && !Sizes[0].Contains("=")) return Sizes[0];     //## 1+ elements in the Array and doesn't have '='- return only whatever there! Any Garbage!

            StringBuilder sizeList = new StringBuilder();

            foreach (var item in Sizes)
            {
                sizeList.Append(item.Split('=')[0] + ", ");   //## Split "12R 11223344" and take the Size, ie: '12R' only, add that '12R' to a StringBuilder
            }
            //## Clean the last ', ' in the StringBuilder and return the result
            return sizeList.ToString()[0..^2];
        }
    }
}