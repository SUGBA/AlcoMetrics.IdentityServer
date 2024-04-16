using IdentityServer.Data.ViewModels;

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
        Task<bool> RegisterUser(RegisterViewModel viewModel);

        /// <summary>
        /// Регистрация любого пользователя (В том числе админа)
        /// </summary>
        /// <param name="viewModel"> Модель регистрируемого пользователя </param>
        /// <returns></returns>
        Task<bool> RegisterAdmin(RegisterAdminViewModel viewModel);

        /// <summary>
        /// Выйти из системы
        /// </summary>
        /// <returns></returns>
        Task<bool> LogOut();
    }
}
