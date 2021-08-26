using Newtonsoft.Json;

namespace DABTechs.eCommerce.Sales.Models.BloomReach
{
    public class Discount
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("count")]
        public string Count { get; set; }
    }
}