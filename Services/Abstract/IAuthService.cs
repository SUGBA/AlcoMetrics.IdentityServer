using IdentityServer.Data.ViewModels;
using Microsoft.AspNetCore.Identity;

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
        Task<IEnumerable<string>> RegisterUser(RegisterViewModel viewModel);

        /// <summary>
        /// Регистрация любого пользователя (В том числе админа)
        /// </summary>
        /// <param name="viewModel"> Модель регистрируемого пользователя </param>
        /// <returns></returns>
        Task<IEnumerable<string>> RegisterAdmin(RegisterAdminViewModel viewModel);
    }
}
