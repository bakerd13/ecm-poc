using Newtonsoft.Json;

namespace DABTechs.eCommerce.Sales.Providers.BloomReach.Models
{
    public class Query
    {
        [JsonProperty("modification")]
        public Modification Modification { get; set; }
    }
}