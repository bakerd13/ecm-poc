using System.Collections.Generic;
using DABTechs.eCommerce.Sales.Common.Enums;
using DABTechs.eCommerce.Sales.Interfaces;

namespace DABTechs.eCommerce.Sales.Business.Models.Product
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

        public Discount? Discount { get; set; }

        public List<PriceList> PriceHistory { get; set; }
        public List<string> Sizes { get; set; }

        public IReadOnlyList<string> SalePrices { get; set; }

        public List<string> SizeDescriptions { get; set; }

        public IEnumerable<ProductSize> GetAvailableSizeOptions()
        {
            var result = new ProductSize[Sizes.Count];
            for (var i = 0; i < Sizes.Count; i++)
            {
                result[i] = new ProductSize
                {
                    Description = SizeDescriptions[i],
                    Price = SalePrices[i],
                    Size = Sizes[i]
                };
            }

            return result;
        }

        public string GetAvailableSizes()
        {
            return Sizes != null ? string.Join(", ", Sizes) : "";
        }

        public string GetDiscountValue()
        {
            // Displays the enumeration entry as a string value, if possible
            var discountValue = Discount.HasValue && Discount != Common.Enums.Discount.UnknownDiscount
                ? Discount.Value.ToString("F")
                : string.Empty;

            return discountValue;
        }
    }
}