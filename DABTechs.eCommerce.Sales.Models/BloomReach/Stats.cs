using Newtonsoft.Json;

namespace DABTechs.eCommerce.Sales.Models.BloomReach
{
    public class Stats
    {
        [JsonProperty("stats_fields")]
        public StatField StatFields { get; set; }
    }
}