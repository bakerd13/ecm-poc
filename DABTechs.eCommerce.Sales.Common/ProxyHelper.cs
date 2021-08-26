using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DABTechs.eCommerce.Sales.Common
{
    public static class ProxyHelper
    {
        public static WebProxy GetDefaultProxy(string proxyAddress)
        {
            var proxy = new WebProxy
            {
                Address = new Uri(proxyAddress),
                BypassProxyOnLocal = false,
                UseDefaultCredentials = true
            };

            return proxy;
        }
    }
}
