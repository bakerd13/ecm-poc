using System.Collections.Generic;
using Newtonsoft.Json;

namespace DABTechs.eCommerce.Sales.Providers.BloomReach.Models
{
    /// <summary>This returns the POCO with list of Facets and Number of each Facet</summary>
    public class FacetFields
    {
        [JsonProperty("sizes")]
        public IList<FacetElementBR> Sizes { get; set; }

        [JsonProperty("brand")]
        public IList<FacetElementBR> Brands { get; set; }

        [JsonProperty("colour")]
        public IList<FacetElementBR> Colours { get; set; }

        [JsonProperty("crumbs_id")]
        public IList<FacetElementBR> CrumbIds { get; set; }

        [JsonProperty("product_type")]
        public IList<FacetElementBR> Categories { get; set; }

        [JsonProperty("gender")]
        public IList<FacetElementBR> Departments { get; set; }

        [JsonProperty("discount")]
        public IList<FacetElementBR> Discount { get; set; }
    }
}