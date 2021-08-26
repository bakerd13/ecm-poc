using DABTechs.eCommerce.Sales.Business.Collections;

namespace DABTechs.eCommerce.Sales.Business.Interfaces
{
    /// <summary>
    /// The Search Provider Results Mapper.
    /// </summary>
    public interface ISearchResultMapper
    {
        /// <summary>
        /// Maps the specified raw result to a SearchResultCollection.
        /// </summary>
        /// <param name="rawResult">The raw result.</param>
        /// <returns></returns>
        SearchResults Map(string rawResult);
    }
}