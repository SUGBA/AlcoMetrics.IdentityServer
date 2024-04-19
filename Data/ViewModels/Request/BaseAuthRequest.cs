using System.Text.Json.Serialization;

namespace IdentityServer.Data.ViewModels.Request
{
    /// <summary>
    /// Базовая модель для аутентификации и регистрации
    /// </summary>
    public abstract class BaseAuthRequest
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
