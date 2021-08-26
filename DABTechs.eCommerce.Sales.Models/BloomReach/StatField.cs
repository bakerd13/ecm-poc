using Newtonsoft.Json;

namespace DABTechs.eCommerce.Sales.Models.BloomReach
{
    public class StatField
    {
        [JsonProperty("sale_price")]
        public SalePrice SalePrices { get; set; }
    }
}