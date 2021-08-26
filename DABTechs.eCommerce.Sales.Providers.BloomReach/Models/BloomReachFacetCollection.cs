using Newtonsoft.Json;

namespace DABTechs.eCommerce.Sales.Providers.BloomReach.Models
{
    public class FacetCollection
    {
        [JsonProperty("facet_ranges")]
        public FacetRanges FacetRanges { get; set; }

        [JsonProperty("facet_fields")]
        public FacetFields FacetFields { get; set; }

        [JsonProperty("facet_queries")]
        public FacetQueries FacetQueries { get; set; }
    }
}