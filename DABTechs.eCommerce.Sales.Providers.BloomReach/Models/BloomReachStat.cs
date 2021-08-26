using Newtonsoft.Json;

namespace DABTechs.eCommerce.Sales.Providers.BloomReach.Models
{
    public class BloomReachStat
    {
        [JsonProperty("sale_price")]
        public SalePrice SalePrices { get; set; }

        [JsonProperty("last_updated")]
        public LastUpdated LastUpdated { get; set; }
    }
}