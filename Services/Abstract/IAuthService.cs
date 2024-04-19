using IdentityServer.Data.ViewModels.Request;
using IdentityServer.Data.ViewModels.Response;

namespace IdentityServer.Services.Abstract
{
    /// <summary>
    /// Сервис для работы с AuthController
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="viewModel"> Модель регистрации </param>
        /// <returns></returns>
        Task<UserRegisterResponse> RegisterUser(RegisterRequest viewModel);

        /// <summary>
        /// Регистрация любого пользователя (В том числе админа)
        /// </summary>
        /// <param name="viewModel"> Модель регистрируемого пользователя </param>
        /// <returns></returns>
        Task<AdminRegisterResponse> RegisterAdmin(RegisterAdminRequest viewModel);
    }
}
