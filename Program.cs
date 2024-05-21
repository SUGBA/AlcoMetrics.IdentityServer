using IdentityServer.Data.Models;
using IdentityServer.DataBase.Contexts;
using IdentityServer.Extensions;
using IdentityServer.IdentityConfiguration;
using IdentityServer.Services;
using IdentityServer.Services.Abstract;
using IdentityServer4;
using IdentityServer4.AspNetIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            #region настраиваем аутентификацию и авторизацию

            builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentityServer(
            options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
           .AddProfileService<ProfileService>()
           .AddInMemoryApiScopes(IdentityConfigurator.ApiScopes)
           .AddInMemoryIdentityResources(IdentityConfigurator.IdentityResources)
           .AddInMemoryApiResources(IdentityConfigurator.ApiResources)
           .AddInMemoryClients(IdentityConfigurator.Clients)
           .AddResourceOwnerValidator<ResourceOwnerPasswordValidator<AuthIdentityUser>>()
           .AddDeveloperSigningCredential()
           .AddJwtBearerClientAuthentication();

            builder.Services.AddIdentity<AuthIdentityUser, AuthIdentityRole>(config =>
            {
                config.Password.RequireDigit = true;
                config.Password.RequiredLength = 6;
                config.Password.RequireUppercase = true;
                config.Password.RequireLowercase = true;
                config.Password.RequireNonAlphanumeric = false;
            })
            .AddErrorDescriber<CustomIdentityErrorDescriber>()
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();

            #endregion

            #region Настраиваем локальную аутентификацию

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityServerConstants.LocalApi.PolicyName;
                options.DefaultChallengeScheme = IdentityServerConstants.LocalApi.PolicyName;
            })
            .AddLocalApi();    

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(IdentityServerConstants.LocalApi.PolicyName, policy =>
                {
                    policy.AddAuthenticationSchemes(IdentityServerConstants.LocalApi.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                });
            });

            #endregion

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddTransient<IAuthService, AuthService>();

            var app = builder.Build();

            await app.ConfigureDataBase();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityServer();
            app.MapControllers();

            app.Run();
        }
    }
}
