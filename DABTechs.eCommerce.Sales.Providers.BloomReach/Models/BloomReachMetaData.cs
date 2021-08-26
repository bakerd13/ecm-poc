using Newtonsoft.Json;

namespace DABTechs.eCommerce.Sales.Providers.BloomReach.Models
{
    public class BloomReachMetaData
    {
        [JsonProperty("query")]
        public Query Query { get; set; }
    }
}