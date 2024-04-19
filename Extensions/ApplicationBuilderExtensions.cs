using IdentityServer.Data.Models;
using IdentityServer.Data.ViewModels.Request;
using IdentityServer.DataBase.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Автомиграции
        /// </summary>
        /// <param name="app"></param>
        public static async Task ConfigureDataBase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbService = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AuthIdentityUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AuthIdentityRole>>();

                if (dbService.Database.GetPendingMigrations().Any())
                    dbService.Database.Migrate();

                if (roleManager.Roles.Count() == 0)
                {
                    //Создаем роли
                    var adminResult = await roleManager.CreateAsync(new AuthIdentityRole() { Name = Roles.Admin.ToString(), NormalizedName = Roles.Admin.ToString().ToUpper(), Id = 1 });
                    if (!adminResult.Succeeded) throw new Exception("Не AuthIdentityRole создать роль администратора");
                    var wineResult = await roleManager.CreateAsync(new AuthIdentityRole() { Name = Roles.WineMaker.ToString(), NormalizedName = Roles.WineMaker.ToString().ToUpper(), Id = 2 });
                    if (!wineResult.Succeeded) throw new Exception("Не удалось создать роль винодела");

                }

                if (userManager.Users.Count() == 0)
                {
                    //Создаем админа
                    var admin = new AuthIdentityUser() { UserName = "admin" };
                    var userResult = await userManager.CreateAsync(admin, "admin");
                    if (!userResult.Succeeded) throw new Exception("Не удалось создать пользователя администратора");
                    var roleResult = await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
                    if (!roleResult.Succeeded) throw new Exception("Не удалось присвоить роль администратора, тестовому админу");
                }
            }
        }
    }
}
