using IdentityServer.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
        Task<bool> Register(RegisterViewModel viewModel);
    }
}
