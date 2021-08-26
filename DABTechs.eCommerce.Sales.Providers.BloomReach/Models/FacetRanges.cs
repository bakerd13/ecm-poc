using System.Collections.Generic;
using Newtonsoft.Json;

namespace DABTechs.eCommerce.Sales.Providers.BloomReach.Models
{
    /// <summary>Price Range gets a List of Price ranges(Array) for various purposes, but we will need only one Price Range</summary>
    public class FacetRanges
    {
        [JsonProperty("price")]
        public IEnumerable<PriceItemDetail> PriceList { get; set; }
    }
}