using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DABTechs.eCommerce.Sales.Business.Interfaces;
using DABTechs.eCommerce.Sales.Business.Collections;
using System;
using System.Threading.Tasks;
using DABTechs.eCommerce.Sales.Interfaces;
using DABTechs.eCommerce.Sales.Business;
using System.Diagnostics;

// TODO add logging
namespace DABTechs.eCommerce.Sales.Search.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VipSearchController : ControllerBase, IVipSearchService
    {
        private readonly ISearchProvider _searchProvider; 
        public VipSearchController(IHttpContextAccessor httpContextAccessor, ISearchProviderFactory searchProviderFactory)
        {
            _searchProvider = searchProviderFactory.GetProvider(httpContextAccessor);        
        }

        [Route("search")]
        [HttpPost]
        public async Task<ActionResult<SearchResults>> GetSearchResultsAsync(FilterQuery filterQuery)
        {
            try
            {
                // Get the API Result.
                string rawResult = await _searchProvider.GetApiResult(filterQuery).ConfigureAwait(false);
                SearchResults searchResults = _searchProvider.Map(rawResult);
                searchResults.FilterQuery = filterQuery;

                // Return the search results.
                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                throw;
            }
        }
    }
}