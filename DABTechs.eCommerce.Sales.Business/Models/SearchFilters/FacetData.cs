using Newtonsoft.Json;
using DABTechs.eCommerce.Sales.Business.Collections;

namespace DABTechs.eCommerce.Sales.Models.SearchFilters
{
    public class FacetData
    {
        public string SerializedFacetData { get; set; }

        public CategoriesCollection CategoriesCollection
        {
            get
            {
                return string.IsNullOrWhiteSpace(SerializedFacetData) ? null : JsonConvert.DeserializeObject<CategoriesCollection>(SerializedFacetData);
            }
        }
    }
}