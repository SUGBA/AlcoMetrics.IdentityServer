using IdentityServer.Data.Models;
using IdentityServer.Data.ViewModels.Request;
using IdentityServer.Data.ViewModels.Response;
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

        private readonly UserManager<AuthIdentityUser> _userManager;

        public AuthController(IAuthService authService, UserManager<AuthIdentityUser> userManager)
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
        public async Task<UserRegisterResponse> RegisterUser([FromBody] RegisterRequest viewModel)
        {
            return await _authService.RegisterUser(viewModel);
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="viewModel"> Модель регистрации </param>
        /// <returns></returns>
        [HttpPost("RegisterAdmin")]
        [Authorize(Roles = "Admin")]
        public async Task<AdminRegisterResponse> RegisterAdmin([FromBody] RegisterAdminRequest viewModel)
        {
            return await _authService.RegisterAdmin(viewModel);
        }
    }
}
