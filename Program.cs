using IdentityModel;
using IdentityServer.Data.Models;
using IdentityServer.DataBase.Contexts;
using IdentityServer.Extensions;
using IdentityServer.IdentityConfiguration;
using IdentityServer.Services;
using IdentityServer.Services.Abstract;
using IdentityServer4;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.AspNetIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

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
           .AddResourceOwnerValidator<ResourceOwnerPasswordValidator<BaseIdentityUser>>()
           .AddDeveloperSigningCredential()
           .AddJwtBearerClientAuthentication();

            builder.Services.AddIdentity<BaseIdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();

            #region Добавляем локальную аутентификацию

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

            app.ConfigureDataBase();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityServer();
            app.MapControllers();

            app.Run();
        }
    }
}
