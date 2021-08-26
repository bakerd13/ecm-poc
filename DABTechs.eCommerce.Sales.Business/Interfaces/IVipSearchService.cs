using Microsoft.AspNetCore.Mvc;
using DABTechs.eCommerce.Sales.Business;
using DABTechs.eCommerce.Sales.Business.Collections;
using System.Threading.Tasks;

namespace DABTechs.eCommerce.Sales.Interfaces
{
    public interface IVipSearchService
    {
        Task<ActionResult<SearchResults>> GetSearchResultsAsync(FilterQuery filterQuery);
    }
}
