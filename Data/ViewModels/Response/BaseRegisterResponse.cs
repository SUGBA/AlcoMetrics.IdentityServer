using System.Text.Json.Serialization;

namespace IdentityServer.Data.ViewModels.Response
{
    /// <summary>
    /// Базовая модель ответа после регистрации
    /// </summary>
    public abstract class BaseRegisterResponse
    {
        /// <summary>
        /// Список ошибок
        /// </summary>
        [JsonPropertyName("Errors")]
        public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();

        /// <summary>
        /// Id добавленного пользователя в случае успеха
        /// </summary>
        [JsonPropertyName("UserId")]
        public int UserId { get; set; }
    }
}
