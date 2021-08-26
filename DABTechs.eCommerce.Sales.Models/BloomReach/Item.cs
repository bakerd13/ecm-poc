using System.Collections.Generic;
using Newtonsoft.Json;

namespace DABTechs.eCommerce.Sales.Models.BloomReach
{
    public class Item
    {
        [JsonProperty("sale_price")]
        public double SalePrice { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; } //## No need to map.. we are not using PDP..

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("pid")]
        public string PID { get; set; }

        [JsonProperty("variant_size")]
        public string[] VariantSize { get; set; }

        [JsonProperty("thumb_image")]
        public string ThumbImage { get; set; }  //## No need to map./. not using any Thumb image, rather a full image

        [JsonProperty("sale_price_range")]
        public string[] SalePriceRange { get; set; }

        [JsonProperty("price_history")]
        public string[] PriceHistory { get; set; }

        [JsonProperty("price_range")]
        public string[] PriceRange { get; set; }

        [JsonProperty("variants")]
        public IList<Variant> Variants { get; set; }

        [JsonProperty("skuid")]
        public string SKUID { get; set; }

        [JsonProperty("large_image")]
        public string[] LargeImage { get; set; }
    }
}