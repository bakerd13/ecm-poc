using Newtonsoft.Json;

namespace DABTechs.eCommerce.Sales.Providers.BloomReach.Models
{
    public class BloomReachStats
    {
        [JsonProperty("stats_fields")]
        public BloomReachStat StatFields { get; set; }

        [JsonProperty("relaxed.query")]
        public string[] QueryWord { get; set; }
    }
}