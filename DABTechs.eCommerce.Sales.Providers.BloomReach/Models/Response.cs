using Newtonsoft.Json;

namespace DABTechs.eCommerce.Sales.Providers.BloomReach.Models
{
    public class Response
    {
        [JsonProperty("numFound")]
        public int TotalResults { get; set; }

        /// <summary>This gives Start Item number, need to divide by 24 to get Current Page</summary>
        [JsonProperty("start")]
        public int CurrentPage { get; set; }

        [JsonProperty("docs")]
        public BloomReachItem[] Items { get; set; }
    }
}