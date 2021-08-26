using Newtonsoft.Json;

namespace DABTechs.eCommerce.Sales.Models.BloomReach
{
    /// <summary>Size Variant- Each size will have its own Price, Image and SKUID</summary>
    public class Variant
    {
        [JsonProperty("skuid")]
        public string SKUID { get; set; }

        [JsonProperty("variant_size")]
        public string[] VariantSize { get; set; }

        [JsonProperty("sku_swatch_images")]
        public string[] SwatchImages { get; set; }

        [JsonProperty("sku_thumb_images")]
        public string[] ThumbImages { get; set; }

        [JsonProperty("variant_price")]
        public string[] OriginalPrice { get; set; }

        [JsonProperty("variant_sale_price")]
        public string[] SalePrice { get; set; }

        [JsonProperty("total_stock_balance")]
        public string[] StockBalance { get; set; }
    }
}