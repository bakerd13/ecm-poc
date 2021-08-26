using System.Collections.Generic;
using Newtonsoft.Json;
using DABTechs.eCommerce.Sales.Domain.BloomReach;

namespace DABTechs.eCommerce.Sales.Models.BloomReach
{
    /// <summary>This returns the POCO with list of Facets and Number of each Facet</summary>
    public class FacetFields
    {
        [JsonProperty("size_facet")]
        public IList<FacetElementBR> Sizes { get; set; }

        [JsonProperty("brand")]
        public IList<FacetElementBR> Brands { get; set; }

        [JsonProperty("colors")]
        public IList<FacetElementBR> Colours { get; set; }

        [JsonProperty("color_groups")]
        public IList<FacetElementBR> ColorGroups { get; set; }

        [JsonProperty("crumbs_id")]
        public IList<FacetElementBR> CrumbIds { get; set; }

        [JsonProperty("category")]
        public IList<FacetElementBR> Categories { get; set; }

        [JsonProperty("department")]
        public IList<FacetElementBR> Departments { get; set; }

        [JsonProperty("variants.variant.discount")]
        public IList<Discount> Discount { get; set; }
    }
}