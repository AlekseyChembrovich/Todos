using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Todos.AuthApi.Configuration;

public class IdentityServer4Configuration
{
    public static IEnumerable<ApiScope> ApiScopes = new List<ApiScope>
    {
        new ApiScope("first_scope")
    };

    public static IEnumerable<ApiResource> ApiResources = new List<ApiResource>
    {
        new ApiResource("first_scope", "first_scope", new[] { JwtClaimTypes.Role })
        {
            Scopes = { "first_scope" }
        }
    };

    public static IEnumerable<IdentityResource> IdentityResources = new List<IdentityResource>
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
    };

    public static IEnumerable<Client> Clients = new List<Client>
    {
        new Client
        {
            ClientId = "angular",

            AllowedGrantTypes = GrantTypes.Code,
            RequireClientSecret = false,
            RequirePkce = true,

            RedirectUris = { "http://localhost:4200" },
            PostLogoutRedirectUris = { "http://localhost:4200" },
            AllowedCorsOrigins = { "http://localhost:4200" },

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
