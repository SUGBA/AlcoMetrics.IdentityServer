﻿using IdentityServer.Data.Models;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Security.Claims;
using IdentityServer4.Extensions;
using IdentityModel;

namespace IdentityServer.IdentityConfiguration
{
    /// <summary>
    /// Профиль для добавления user claims
    /// </summary>
    public class ProfileService : IProfileService
    {
        private const string ROLES_CLAIM_NAME = "roles";

        private const string USER_NAME_CLAIM_NAME = "name";

        private readonly UserManager<BaseIdentityUser> _userManager;

        public ProfileService(UserManager<BaseIdentityUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Добавление claims
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var roles = await GetRolesClaims(context);
            roles.ForEach(role => context.IssuedClaims.Add(role));

            context.IssuedClaims.Add(await GetNameClaim(context));
        }

        /// <summary>
        /// Проверка возможности добавления claims
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }

        /// <summary>
        /// Получить пользовательский роли в виде Claims
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private async Task<List<Claim>> GetRolesClaims(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            if (user == null) throw new ArgumentException("Пользователь не найден, при попытке добавить роли в userClaims");

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>();
            foreach (var role in roles)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, role));
            }

            return claims;
        }

        /// <summary>
        /// Добавить claim с именем пользователя
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task<Claim> GetNameClaim(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            if (user == null) throw new ArgumentException("Пользователь не найден, при попытке добавить имя пользователя в userClaims");

            var result = new Claim(USER_NAME_CLAIM_NAME, user.UserName);

            return result;
        }
    }
}
