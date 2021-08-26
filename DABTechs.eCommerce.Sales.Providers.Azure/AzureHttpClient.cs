using System;
using System.Net.Http;
using Microsoft.Extensions.Options;
using DABTechs.eCommerce.Sales.Common.Config;

namespace DABTechs.eCommerce.Sales.Providers.Azure
{
    public class AzureHttpClient
    {
        public HttpClient Client { get; }

        public AzureHttpClient(HttpClient client, IOptions<AppSettings> appSettings)
        {
            client.BaseAddress = new Uri(appSettings.Value.AzureApi);

            Client = client;
        }
    }
}