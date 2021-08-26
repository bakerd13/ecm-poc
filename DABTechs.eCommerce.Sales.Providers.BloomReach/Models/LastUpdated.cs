using Newtonsoft.Json;

namespace DABTechs.eCommerce.Sales.Providers.BloomReach.Models
{
    public class LastUpdated
    {
        [JsonProperty("min")]
        public double MinimumLastUpdated { get; set; }

        [JsonProperty("max")]
        public double MaximumLastUpdated { get; set; }
    }
}