using IdentityServer.Data.Models;
using IdentityServer.Data.ViewModels;
using IdentityServer.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    /// <summary>
    /// Контроллера для атуентификации
    /// </summary>
    [Route("Auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        private readonly UserManager<BaseIdentityUser> _userManager;

        public AuthController(IAuthService authService, UserManager<BaseIdentityUser> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="viewModel"> Модель регистрации </param>
        /// <returns></returns>
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterViewModel viewModel)
        {
            var result = await _authService.RegisterUser(viewModel);
            return result ? StatusCode(StatusCodes.Status200OK) : StatusCode(StatusCodes.Status400BadRequest);
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="viewModel"> Модель регистрации </param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminViewModel viewModel)
        {
            var result = await _authService.RegisterAdmin(viewModel);
            return result ? StatusCode(StatusCodes.Status200OK) : StatusCode(StatusCodes.Status400BadRequest);
        }

        /// <summary>
        /// Выйти из системы
        /// </summary>
        /// <returns></returns>
        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            var result = await _authService.LogOut();
            return result ? StatusCode(StatusCodes.Status200OK) : StatusCode(StatusCodes.Status400BadRequest);
        }
    }
}
