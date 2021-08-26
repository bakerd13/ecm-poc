using Newtonsoft.Json;

namespace DABTechs.eCommerce.Sales.Providers.BloomReach.Models
{
    public class SalePrice
    {
        [JsonProperty("min")]
        public double MinimumMarkPrice { get; set; }

        [JsonProperty("max")]
        public double MaximumMarkPrice { get; set; }
    }
}