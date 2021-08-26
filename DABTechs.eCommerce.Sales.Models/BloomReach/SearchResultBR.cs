using Newtonsoft.Json;

namespace DABTechs.eCommerce.Sales.Models.BloomReach
{
    public class SearchResultBR
    {
        [JsonProperty("response")]
        public Response Response { get; set; }

        [JsonProperty("facet_counts")]
        public FacetsObject Facets { get; set; }

        [JsonProperty("did_you_mean")]
        public string[] Suggestion { get; set; }

        [JsonProperty("stats")]
        public Stats Stats { get; set; }
    }
}