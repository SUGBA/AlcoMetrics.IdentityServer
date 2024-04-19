using IdentityServer4.Models;
using IdentityServer4;
using IdentityModel;

namespace IdentityServer.IdentityConfiguration
{
    /// <summary>
    /// Конфигурация IdentityServer
    /// </summary>
    public static class IdentityConfigurator
    {
        /// <summary>
        /// Добавляем пространства к которым будет предоставлен доступ
        /// </summary>
        public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("AlcoMetrics.Wine.Backend.Default"),
            new ApiScope("AlcoMetrics.Backend.Default")
        };

        /// <summary>
        /// Добавляем способы по которым, будет индентифицирован пользователь
        /// </summary>
        public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
        };

        /// <summary>
        /// Добавляем API
        /// </summary>
        public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            new ApiResource("AlcoMetrics.Wine.Backend", "WebApi for Wine Service")
            {
               Scopes = new List<string>{ "AlcoMetrics.Wine.Backend.Default" },
               ApiSecrets = new List<Secret>{ new Secret("secre_#$forWineApi17782_ahseasd2_$231zmnkmtslaf12&&/".Sha256()) }
            },
            new ApiResource("AlcoMetrics.Backend", "WebApi for main agregate Service")
            {
               Scopes = new List<string>{ "AlcoMetrics.Backend.Default" },
               ApiSecrets = new List<Secret>{ new Secret("djashvdjSecretttt_dahjsbdjaFORaakjsdafACLOMETRIC.BACKEND123asjhdv$".Sha256()) }
            },
        };

        /// <summary>
        /// Определение клиента
        /// </summary>
        public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "wine_web_user",
                ClientName = "Wine Analyzer Web User",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AccessTokenType = AccessTokenType.Jwt,
                ClientSecrets = { new Secret("@win3_auth_s33cret123$123WEbuuserCclient41".Sha256())},
                AllowedScopes =
                {
                    "AlcoMetrics.Wine.Backend.Default",
                    "AlcoMetrics.Backend.Default",
                },
                AllowAccessTokensViaBrowser = true
            }
        };
    }
}
