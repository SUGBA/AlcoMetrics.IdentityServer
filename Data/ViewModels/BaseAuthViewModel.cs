using System.Text.Json.Serialization;

namespace IdentityServer.Data.ViewModels
{
    /// <summary>
    /// Базовая модель для аутентификации и регистрации
    /// </summary>
    public abstract class BaseAuthViewModel
    {
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Логин пользователя
        /// </summary>
        [JsonPropertyName("login")]
        public string Login { get; set; } = string.Empty;
    }
}
