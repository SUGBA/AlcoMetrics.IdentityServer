using IdentityServer.Data.Models;
using IdentityServer.DataBase.Contexts;
using IdentityServer.Extensions;
using IdentityServer.IdentityConfiguration;
using IdentityServer.Services;
using IdentityServer.Services.Abstract;
using IdentityServer4.AspNetIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentityServer(
                          options =>
                          {
                              options.Events.RaiseErrorEvents = true;
                              options.Events.RaiseFailureEvents = true;
                              options.Events.RaiseInformationEvents = true;
                              options.Events.RaiseSuccessEvents = true;
                          }
                )
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

            builder.Services.AddControllers();

            builder.Services.AddTransient<IAuthService, AuthService>();

            var app = builder.Build();

            app.ConfigureDataBase();
            app.UseRouting();
            app.UseIdentityServer();
            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}
