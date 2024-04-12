using IdentityServer.DataBase.Contexts;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Автомиграции
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureDataBase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbService = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();

                if (dbService.Database.GetPendingMigrations().Any())
                    dbService.Database.Migrate();
            }
        }
    }
}
