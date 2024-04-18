namespace GostCryptography.Client.Exceptions
{
    /// <summary>
    /// Репрезентация ошибки API
    /// </summary>
    public class ApiRequestException : RequestException
    {
        /// <summary>
        /// HTTP-код ошибки
        /// </summary>
        public virtual int HTTPStatus { get; }

        /// <summary>
        /// Код ошибки API
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Инициализация нового экземпляра <see cref="ApiRequestException"/>
        /// </summary>
        /// <param name="message"></param>
        public ApiRequestException(string message)
            : base(message)
        { }

        /// <summary>
        /// Инициализация нового экземпляра <see cref="ApiRequestException"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="httpStatus"></param>
        public ApiRequestException(string message, int httpStatus)
            : base(message) =>
            HTTPStatus = httpStatus;

        /// <summary>
        /// Инициализация нового экземпляра <see cref="ApiRequestException"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ApiRequestException(string message, Exception innerException)
            : base(message, innerException)
        { }

        /// <summary>
        /// Инициализация нового экземпляра <see cref="ApiRequestException"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="httpStatus"></param>
        /// <param name="innerException"></param>
        public ApiRequestException(string message, int httpStatus, Exception innerException)
            : base(message, innerException) =>
            HTTPStatus = httpStatus;

        /// <summary>
        /// Инициализация нового экземпляра <see cref="ApiRequestException"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="httpStatus"></param>
        /// <param name="code"></param>
        /// <param name="moreInfo"></param>
        public ApiRequestException(
            string message,
            int httpStatus,
            string code)
            : base(message)
        {
            HTTPStatus = httpStatus;
            Code = code;
        }

        /// <summary>
        /// Инициализация нового экземпляра <see cref="ApiRequestException"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="httpStatus"></param>
        /// <param name="code"></param>
        /// <param name="moreInfo"></param>
        /// <param name="innerException"></param>
        public ApiRequestException(
            string message,
            int httpStatus,
            string code,
            Exception innerException)
            : base(message, innerException)
        {
            HTTPStatus = httpStatus;
            Code = code;
        }
    }
}
