using System.Text.Json.Serialization;

namespace IdentityServer.Data.ViewModels.Request
{
    /// <summary>
    /// Модель для регистрации пользователя
    /// </summary>
    public class RegisterRequest : BaseAuthRequest
    {
        /// <summary>
        /// Пользовательская роль
        /// </summary>
        [JsonPropertyName("UserRole")]
        public Roles UserRole { get; set; }
    }

    /// <summary>
    /// Enum с ролями
    /// </summary>
    public enum Roles : byte
    {
        /// <summary>
        /// Винодел
        /// </summary>
        Admin = 1,

        /// <summary>
        /// Винодел
        /// </summary>
        WineMaker = 2
    }
}
