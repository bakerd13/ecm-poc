using System.Linq;
using Newtonsoft.Json;

namespace DABTechs.eCommerce.Sales.Providers.BloomReach.Models
{
    /// <summary>Size Variant- Each size will have its own Price, Image and SKUID</summary>
    public class BloomReachVariant
    {
        [JsonProperty("skuid")]
        public string SKUID { get; set; }

        [JsonProperty("size_code")]
        public string[] SizeCode { get; set; }

        [JsonProperty("size_desc")]
        public string[] SizeDescription { get; set; }

        [JsonProperty("size_facet")]
        public string[] SizeFacet { get; set; }

        [JsonProperty("variant_size")]
        public string[] VariantSize { get; set; }

        [JsonProperty("sku_swatch_images")]
        public string[] SwatchImages { get; set; }

        [JsonProperty("sku_thumb_images")]
        public string[] ThumbImages { get; set; }

        /// <summary>
        /// variant_price
        /// </summary>
        /// <value>
        /// The original price.
        /// </value>
        [JsonProperty("variant_price")]
        public string[] OriginalPrice { get; set; }

        /// <summary>
        /// variant_sale_price
        /// </summary>
        /// <value>
        /// The sale price.
        /// </value>
        [JsonProperty("variant_sale_price")]
        public string[] SalePrice { get; set; }

        [JsonProperty("total_stock_balance")]
        public string[] StockBalance { get; set; }

        [JsonProperty("discount")]
        public string[] Discount { get; set; }

        [JsonProperty("price_history")]
        public string[] PriceHistory { get; set; }

        [JsonIgnore]
        public double OriginalPriceValue
        {
            get
            {
                double.TryParse(OriginalPrice.FirstOrDefault(), out double result);
                return result;
            }
        }

        [JsonIgnore]
        public double SalePriceValue
        {
            get
            {
                double.TryParse(SalePrice.FirstOrDefault(), out double result);
                return result;
            }
        }
    }
}