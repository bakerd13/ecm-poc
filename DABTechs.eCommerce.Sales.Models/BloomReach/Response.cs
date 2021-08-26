using System.Collections.Generic;
using Newtonsoft.Json;

namespace DABTechs.eCommerce.Sales.Models.BloomReach
{
    /// <summary>Response object from BR has all the details about Products and Search summary</summary>
    public class Response
    {
        [JsonProperty("numFound")]
        public int TotalResults { get; set; }

        /// <summary>This gives Start Item number, need to divide by 24 to get Current Page</summary>
        [JsonProperty("start")]
        public int CurrentPage { get; set; }

        [JsonProperty("docs")]
        public List<Item> Items { get; set; }
    }
}