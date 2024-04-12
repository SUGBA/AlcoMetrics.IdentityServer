using IdentityServer.Data.Models;
using IdentityServer.Data.ViewModels;
using IdentityServer.Services.Abstract;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Services
{
    /// <summary>
    /// Сервис для работы с AuthController
    /// </summary>
    public class AuthService : IAuthService
    {

        private readonly UserManager<BaseIdentityUser> _userManager;

        public AuthService(UserManager<BaseIdentityUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Регистрация 
        /// </summary>
        /// <param name="model"> Модель для регистрации </param>
        /// <returns>True - Успех/False - провал</returns>
        public async Task<bool> Register(RegisterViewModel viewModel)
        {
            var user = new BaseIdentityUser { UserName = viewModel.Login };

            var result = await _userManager.CreateAsync(user, viewModel.Password);
            if (!result.Succeeded)
                return false;

            return true;
        }
    }
}
