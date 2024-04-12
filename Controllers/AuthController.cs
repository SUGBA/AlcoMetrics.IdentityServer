using IdentityServer.Data.Models;
using IdentityServer.Data.ViewModels;
using IdentityServer.Services.Abstract;
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
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel viewModel)
        {
            var result = await _authService.Register(viewModel);
            return result ? StatusCode(StatusCodes.Status200OK) : StatusCode(StatusCodes.Status400BadRequest);
        }
    }
}
