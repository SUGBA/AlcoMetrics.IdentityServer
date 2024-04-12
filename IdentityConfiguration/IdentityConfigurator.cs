using IdentityServer4.Models;
using IdentityServer4;

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
            new ApiScope("WineApi.default"),        //Доступ к WineWebApi
        };

        /// <summary>
        /// Добавляем способы по которым, будет индентифицирован пользователь
        /// </summary>
        public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

        /// <summary>
        /// Добавляем API
        /// </summary>
        public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            new ApiResource("WineApi")
            {
               Scopes = new List<string>{ "WineApi.default" },
               ApiSecrets = new List<Secret>{ new Secret("secre_#$forWineApi17782_ahseasd2_$231zmnkmtslaf12&&/".Sha256()) }
            }
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
                RequirePkce = true,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "WineApi.default"
                },
                AllowAccessTokensViaBrowser = true
            }
        };
    }
}
