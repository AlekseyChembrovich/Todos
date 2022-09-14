using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Todos.AuthApi.Configuration;

public class IdentityServer4Configuration
{
    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new List<ApiScope>
        {
            new ApiScope("first_scope")
        };
    }

    public static IEnumerable<ApiResource> GetApiResources()
    {
        return new List<ApiResource>
        {
            new ApiResource("first_scope", "first_scope", new[] { JwtClaimTypes.Role, JwtClaimTypes.Name })
            {
                Scopes = { "first_scope" }
            }
        };
    }

    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    }

    public static IEnumerable<Client> GetClients(IConfiguration configuration)
    {
        var client = configuration.GetValue<string>("AuthConfig:Client");
        return new List<Client>
        {
            new Client
            {
                ClientId = "angular",

                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                RequirePkce = true,

                RedirectUris = { client },
                PostLogoutRedirectUris = { client },
                AllowedCorsOrigins = { client },

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "first_scope"
                },

                AllowAccessTokensViaBrowser = true,
                RequireConsent = false
            }
        };
    }
}
