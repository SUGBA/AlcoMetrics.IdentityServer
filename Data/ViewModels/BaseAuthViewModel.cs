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
        [JsonPropertyName("Password")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Логин пользователя
        /// </summary>
        [JsonPropertyName("Login")]
        public string Login { get; set; } = string.Empty;
    }
}
