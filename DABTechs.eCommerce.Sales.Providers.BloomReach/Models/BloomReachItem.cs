using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace DABTechs.eCommerce.Sales.Providers.BloomReach.Models
{
    public class BloomReachItem
    {
        [JsonProperty("sale_price")]
        public double SalePrice { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("special_offer")]
        public bool SpecialOffer { get; set; }

        [JsonProperty("pid")]
        public string PID { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("further_reduction")]
        public bool FurtherReduction { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("product_type")]
        public string ProductType { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("sale_price_range")]
        public string[] SalePriceRange { get; set; }

        [JsonProperty("price_history")]
        public string[] PriceHistory { get; set; }

        [JsonProperty("price_range")]
        public string[] PriceRange { get; set; }

        [JsonProperty("skuid")]
        public string SKUID { get; set; }

        [JsonProperty("large_image")]
        public string[] LargeImage { get; set; }

        [JsonProperty("variants")]
        public BloomReachVariant[] Variants { get; set; }

        public IList<string> GiveSizeDescriptions()
        {
            List<string> result = new List<string>();

            foreach (BloomReachVariant variant in this.Variants)
            {
                string variantSize = variant.SizeDescription?.FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(variantSize))
                {
                    result.Add(variantSize);
                }
                else
                {
                    variantSize = variant.VariantSize.FirstOrDefault();
                    if (string.IsNullOrWhiteSpace(variantSize)) { continue; }
                    string[] variantSizePair = variantSize.Split('=');
                    result.Add(variantSizePair[0]);
                }
            }

            return result;
        }

        public IList<string> GiveVariantSizes()
        {
            List<string> result = new List<string>();

            foreach (BloomReachVariant variant in this.Variants)
            {
                string variantSize = variant.VariantSize.FirstOrDefault();
                if (string.IsNullOrWhiteSpace(variantSize)) { continue; }
                string[] variantSizePair = variantSize.Split('=');
                result.Add(variantSizePair[0]);
            }

            return result;
        }

        public IList<string> GiveSizeOptions()
        {
            List<string> result = new List<string>();

            foreach (BloomReachVariant variant in this.Variants)
            {
                string variantSize = variant.SizeCode.FirstOrDefault();
                if (string.IsNullOrWhiteSpace(variantSize)) { continue; }
                string[] variantSizePair = variantSize.Split('=');
                result.Add(variantSizePair[0]);
            }

            return result;
        }

        public IList<string> GiveSalePrices()
        {
            List<string> result = new List<string>();

            foreach (BloomReachVariant varient in Variants)
            {
                string salePrice = varient.SalePrice.FirstOrDefault();
                result.Add(salePrice);
            }

            return result;
        }

        public string GiveDiscount()
        {
            var variant = Variants.FirstOrDefault();
            if (variant == null) { return string.Empty; }
            return variant.Discount.FirstOrDefault();
        }

        public double GiveMinSalePrice()
        {
            return Variants.Min(v => v.SalePriceValue);
        }

        public double GiveMaxSalePrice()
        {
            return Variants.Max(v => v.SalePriceValue);
        }

        public double GiveMaxOriginalPrice()
        {
            return Variants.Max(v => v.OriginalPriceValue);
        }

        public double GiveMinOriginalPrice()
        {
            return Variants.Min(v => v.OriginalPriceValue);
        }

        public string[] GivePriceHistory()
        {
            return Variants.FirstOrDefault()?.PriceHistory;
        }

        public override string ToString()
        {
            return $"{PID} {Title}";
        }
    }
}