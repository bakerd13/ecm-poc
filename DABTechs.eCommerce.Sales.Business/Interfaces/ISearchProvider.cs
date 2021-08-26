using DABTechs.eCommerce.Sales.Business.Collections;
using DABTechs.eCommerce.Sales.Common.Enums;
using System.Threading.Tasks;

namespace DABTechs.eCommerce.Sales.Business.Interfaces
{
    public interface ISearchProvider
    {
        SearchProvider SearchProviderType { get; }

        /// <summary>
        /// Gets the raw result string from the search provider.
        /// </summary>
        /// <param name="filterQuery">The search query.</param>
        /// <returns></returns>
        Task<string> GetApiResult(FilterQuery filterQuery);


        /// <summary>
        /// Map the raw result to the SearchResultCollection.
        /// </summary>
        /// <param name="rawResult">The raw result.</param>
        /// <returns></returns>
        SearchResults Map(string rawResult);
    }
}
