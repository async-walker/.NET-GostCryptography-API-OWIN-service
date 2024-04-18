namespace GostCryptography.Client.Exceptions
{
    /// <summary>
    /// Парсинг исключений неуспешных ответов от API
    /// </summary>
    public interface IExceptionParser
    {
        /// <summary>
        /// Парсинг HTTP ответа с исключением
        /// </summary>
        /// <param name="error">Ошибка API с ответом</param>
        /// <returns>Экземпляр <see cref="ApiRequestException"/></returns>
        ApiRequestException Parse(ApiResponseError error);
    }
}
