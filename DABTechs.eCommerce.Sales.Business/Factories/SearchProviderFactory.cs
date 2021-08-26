using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using DABTechs.eCommerce.Sales.Business.Interfaces;
using DABTechs.eCommerce.Sales.Common.Config;
using System.Collections.Generic;
using System.Linq;

namespace DABTechs.eCommerce.Sales.Business.Factories
{
    public class SearchProviderFactory : ISearchProviderFactory
    {
        private readonly VipSearchApiSettings _appSettings;
        private readonly IEnumerable<ISearchProvider> _searchProviders;

        public SearchProviderFactory(IEnumerable<ISearchProvider> searchProviders, IOptions<VipSearchApiSettings> appSettings)
        {
            _searchProviders = searchProviders;
            _appSettings = appSettings.Value;
        }

        public ISearchProvider GetProvider(IHttpContextAccessor httpContextAccessor)
        {
            var searchProviderName = _appSettings.SearchProviderName;

            var providerOverride = httpContextAccessor.HttpContext.Request.Cookies["provider"];
            if (string.IsNullOrWhiteSpace(providerOverride))
            {
                providerOverride = httpContextAccessor.HttpContext.Request.Headers["PROVIDER"];
            }

            if (string.IsNullOrWhiteSpace(providerOverride))
            {
                providerOverride = httpContextAccessor.HttpContext.Request.Query["PROVIDER"];
            }

            if (!string.IsNullOrWhiteSpace(providerOverride))
            {
                searchProviderName = providerOverride.Trim();
            }

            var selectedSearchProvider = ((searchProviderName ?? string.Empty).ToLower()) switch
            {
                "azure" => Common.Enums.SearchProvider.Azure,

                "sli" => Common.Enums.SearchProvider.SLI,

                "bloomreach" => Common.Enums.SearchProvider.BloomReach,

                _ => Common.Enums.SearchProvider.BloomReach,
            };

            return _searchProviders.FirstOrDefault(x => x.SearchProviderType == selectedSearchProvider);
        }
    }
}
