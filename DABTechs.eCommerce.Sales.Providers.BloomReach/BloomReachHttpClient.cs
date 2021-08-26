using System;
using System.Net.Http;
using Microsoft.Extensions.Options;
using DABTechs.eCommerce.Sales.Common.Config;

namespace DABTechs.eCommerce.Sales.Providers.BloomReach
{
    public class BloomReachHttpClient
    {
        public HttpClient Client { get; }

        public BloomReachHttpClient(HttpClient client, IOptions<AppSettings> appSettings)
        {
            client.BaseAddress = new Uri(appSettings.Value.BloomreachApi);

            Client = client;
        }
    }
}