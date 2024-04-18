namespace GostCryptography.Client.Exceptions
{
    /// <summary>
    /// Стандартный объект для парсинга исключений из тела HTTP-ответа
    /// </summary>
    public class DefaultExceptionParser : IExceptionParser
    {
        /// <inheritdoc/>
        public ApiRequestException Parse(ApiResponseError apiResponse)
        {
            return apiResponse is null
                ? throw new ArgumentNullException(nameof(apiResponse))
                : new(
                    message: apiResponse.Message,
                    httpStatus: apiResponse.StatusCode,
                    code: apiResponse.Code);
        }
    }
}
