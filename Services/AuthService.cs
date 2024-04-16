using IdentityServer.Data.Models;
using IdentityServer.Data.ViewModels;
using IdentityServer.Services.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Services
{
    /// <summary>
    /// Сервис для работы с AuthController
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly UserManager<BaseIdentityUser> _userManager;

        private readonly IHttpContextAccessor _contextAccessor;

        public AuthService(UserManager<BaseIdentityUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        /// <summary>
        /// Выйти из системы
        /// </summary>
        /// <returns></returns>
        public async Task<bool> LogOut()
        {
            if (_contextAccessor.HttpContext == null)
                return false;
            await _contextAccessor.HttpContext.SignOutAsync();
            return true;
        }

        /// <summary>
        /// Регистрация любого пользователя кроме админа
        /// </summary>
        /// <param name="viewModel"> Модель для регистрации </param>
        /// <returns>True - Успех/False - провал</returns>
        public async Task<bool> RegisterUser(RegisterViewModel viewModel)
        {
            //Для регистрации админа выделена отдельная защищенная точка
            if (viewModel.UserRole == Roles.Admin)
                return false;

            var user = new BaseIdentityUser { UserName = viewModel.Login };

            var userResult = await _userManager.CreateAsync(user, viewModel.Password);
            if (!userResult.Succeeded)
                return false;

            var roleResult = await _userManager.AddToRoleAsync(user, viewModel.UserRole.ToString());
            if (!roleResult.Succeeded)
                return false;

            return true;
        }

        /// <summary>
        /// Регистрация любого пользователя (В том числе админа)
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task<bool> RegisterAdmin(RegisterAdminViewModel viewModel)
        {
            var user = new BaseIdentityUser { UserName = viewModel.Login };

            var userResult = await _userManager.CreateAsync(user, viewModel.Password);
            if (!userResult.Succeeded)
                return false;

            var roleResult = await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            if (!roleResult.Succeeded)
                return false;

            return true;
        }
    }
}
