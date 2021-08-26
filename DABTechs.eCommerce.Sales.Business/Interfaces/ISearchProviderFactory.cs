using Microsoft.AspNetCore.Http;

namespace DABTechs.eCommerce.Sales.Business.Interfaces
{
    public interface ISearchProviderFactory
    {
        ISearchProvider GetProvider(IHttpContextAccessor httpContextAccessor);
    }
}
