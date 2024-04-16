﻿using IdentityServer.Data.Models;
using IdentityServer.Data.ViewModels;
using IdentityServer.Services.Abstract;
using IdentityServer4;
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
        public async Task<IEnumerable<string>> RegisterUser([FromBody] RegisterViewModel viewModel)
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
        public async Task<IEnumerable<string>> RegisterAdmin([FromBody] RegisterAdminViewModel viewModel)
        {
            return await _authService.RegisterAdmin(viewModel);
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
