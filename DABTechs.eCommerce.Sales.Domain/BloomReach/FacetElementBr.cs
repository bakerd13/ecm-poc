using Newtonsoft.Json;

namespace DABTechs.eCommerce.Sales.Domain.BloomReach
{
    public class FacetElementBR
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        public string Title { get; set; }

        public bool Selected { get; set; }

        public virtual string GetCount() => string.Concat('(', Count, ')');
    }
}