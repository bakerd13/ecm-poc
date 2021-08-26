using Newtonsoft.Json;

namespace DABTechs.eCommerce.Sales.Models.BloomReach
{
    public class SalePrice
    {
        [JsonProperty("min")]
        public double MinimumMarkPrice { get; set; }

        [JsonProperty("max")]
        public double MaximumMarkPrice { get; set; }
    }
}