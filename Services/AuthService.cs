using IdentityServer.Data.Models;
using IdentityServer.Data.ViewModels.Request;
using IdentityServer.Data.ViewModels.Response;
using IdentityServer.Services.Abstract;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Services
{
    /// <summary>
    /// Сервис для работы с AuthController
    /// </summary>
    public class AuthService : IAuthService
    {
        /// <summary>
        /// Ошибка при попытке создать пользователя в ендпоинте для создания пользователей
        /// </summary>
        private const string CREATE_ADMIN_ERROR = "Конечная точка не предусмтаривает создание администратора";

        /// <summary>
        /// Ошибка если пользователь не был создан. Кейс вроде как невозможный, но мало-ли
        /// </summary>
        private const string CREATE_USER_DB_ERROR = "Пользователя не удалось создать";

        private readonly UserManager<AuthIdentityUser> _userManager;

        public AuthService(UserManager<AuthIdentityUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Регистрация любого пользователя кроме админа
        /// </summary>
        /// <param name="viewModel"> Модель для регистрации </param>
        /// <returns>True - Успех/False - провал</returns>
        public async Task<UserRegisterResponse> RegisterUser(RegisterRequest viewModel)
        {
            //Для регистрации админа выделена отдельная защищенная точка
            if (viewModel.UserRole == Roles.Admin)
                return new UserRegisterResponse() { Errors = new List<string>() { CREATE_ADMIN_ERROR } };

            var user = new AuthIdentityUser { UserName = viewModel.Login };

            var userResult = await _userManager.CreateAsync(user, viewModel.Password);
            if (!userResult.Succeeded)
                return new UserRegisterResponse() { Errors = userResult.Errors.Select(x => x.Description) };

            var roleResult = await _userManager.AddToRoleAsync(user, viewModel.UserRole.ToString());
            if (!roleResult.Succeeded)
                return new UserRegisterResponse() { Errors = roleResult.Errors.Select(x => x.Description) };

            var createdUser = _userManager.Users.ToList().FirstOrDefault(x=>x.UserName == viewModel.Login);
            if (createdUser == null)
                return new UserRegisterResponse() { Errors = new List<string>() { CREATE_USER_DB_ERROR } };

            return new UserRegisterResponse() { Errors = Enumerable.Empty<string>(), UserId = createdUser.Id };
        }

        /// <summary>
        /// Регистрация любого пользователя (В том числе админа)
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task<AdminRegisterResponse> RegisterAdmin(RegisterAdminRequest viewModel)
        {
            var user = new AuthIdentityUser { UserName = viewModel.Login };

            var userResult = await _userManager.CreateAsync(user, viewModel.Password);
            if (!userResult.Succeeded)
                return new AdminRegisterResponse() { Errors = userResult.Errors.Select(x => x.Description) };

            var roleResult = await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            if (!roleResult.Succeeded)
                return new AdminRegisterResponse() { Errors = roleResult.Errors.Select(x => x.Description) };

            var createdUser = _userManager.Users.FirstOrDefault(user);
            if (createdUser == null)
                return new AdminRegisterResponse() { Errors = new List<string>() { CREATE_USER_DB_ERROR } };

            return new AdminRegisterResponse() { Errors = Enumerable.Empty<string>(), UserId = createdUser.Id };
        }
    }
}
