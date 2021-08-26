using Newtonsoft.Json;

namespace DABTechs.eCommerce.Sales.Models.BloomReach
{
    /// <summary>Facet object returns all the Facets related info</summary>
    public class FacetsObject
    {
        [JsonProperty("facet_ranges")]
        public FacetRanges FacetRanges { get; set; }

        [JsonProperty("facet_fields")]
        public FacetFields FacetFields { get; set; }

        [JsonProperty("facet_queries")]
        public FacetQueries FacetQueries { get; set; }
    }
}