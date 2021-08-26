using Newtonsoft.Json;

namespace DABTechs.eCommerce.Sales.Providers.BloomReach.Models
{
    public class BloomReachModel
    {
        [JsonProperty("response")]
        public Response Response { get; set; }

        [JsonProperty("facet_counts")]
        public FacetCollection Facets { get; set; }

        [JsonProperty("did_you_mean")]
        public string[] Suggestion { get; set; }

        [JsonProperty("stats")]
        public BloomReachStats Stats { get; set; }

        [JsonProperty("metadata")]
        public BloomReachMetaData Metadata { get; set; }
    }
}