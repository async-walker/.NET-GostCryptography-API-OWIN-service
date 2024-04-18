using Newtonsoft.Json;

namespace GostCryptography.Client.Exceptions
{
    /// <summary>
    /// Репрезентация JSON-объекта ошибки REST-методов
    /// </summary>
    /// <remarks>
    /// Инициализация экземпляра <see cref="ApiResponseError"/>
    /// </remarks>
    public class ApiResponseError
    {
        /// <summary>
        /// HTTP статус класса 4xx согласно Hypertext Transfer Protocol (HTTP) Status Code Registry 
        /// </summary>
        [JsonProperty(nameof(StatusCode), Required = Required.Always)]
        public int StatusCode { get; private set; }
        /// <summary>
        /// Внутренний код ошибки Портала. Служит клиенту для автоматизированной обработки ошибок
        /// </summary>
        [JsonProperty(nameof(Code), Required = Required.Always)]
        public string Code { get; private set; } = default!;
        /// <summary>
        /// Расшифровка ошибки. Служит для человеко-читаемой обработки ошибок
        /// </summary>
        [JsonProperty(nameof(Message), Required = Required.Always)]
        public string Message { get; private set; } = default!;
    }
}
