using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using DABTechs.eCommerce.Sales.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DABTechs.eCommerce.Sales.Identity
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> { 
                new ApiResource( SecurityResources.VipSearchApiResource, "Vip Search Api Resource")
                {
                    ApiSecrets = { new Secret(SecurityScopes.VipApiSecret.Sha256()) },
                    Scopes = { SecurityScopes.VipSearchApiScope }
                },
                new ApiResource( SecurityResources.VipMenuApiResource, "Vip Menu Api Resource")
                {
                    ApiSecrets = { new Secret(SecurityScopes.VipApiSecret.Sha256()) },
                    Scopes = { SecurityScopes.VipMenuApiScope }
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope(SecurityScopes.VipSearchApiScope),
                new ApiScope(SecurityScopes.VipMenuApiScope)
            };
        }

        public static List<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource> { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        // TODO add more allowed scopes, put some things into config
        // eg AllowedCorsOrigins
        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new List<Client> { 
                new Client
                {
                    ClientId = SecurityClients.VipWebClientId,
                    ClientName = "Vip Sales Client",
                    ClientSecrets = { new Secret(SecurityClients.VipWebClientSecret.ToSha256()) },
                    RequireClientSecret = false,
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = true,
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = { 
                        IdentityServerConstants.StandardScopes.OpenId, 
                        IdentityServerConstants.StandardScopes.Profile,
                        SecurityScopes.VipSearchApiScope,
                        SecurityScopes.VipMenuApiScope,
                    },
                    AllowedCorsOrigins = new List<string> { "http://sale.dab.localhost" },
                    RedirectUris =  { configuration.GetValue<string>("RedirectUrls:WebClient") }
                }
            };
        }

    }
}
