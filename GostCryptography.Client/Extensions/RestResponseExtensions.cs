using GostCryptography.Client.Exceptions;
using Newtonsoft.Json;
using RestSharp;
using System.Runtime.CompilerServices;

namespace GostCryptography.Client.Extensions
{
    public static class RestResponseExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static TResponseType DeserializeContent<TResponseType>(
        this RestResponse response)
        {
            if (response.Content is null)
            {
                throw new RequestException(
                    message: "Контент ответа ничего в себе не содержит (null)",
                    httpStatusCode: response.StatusCode);
            }

            TResponseType? deserializedObject;

            try
            {
                deserializedObject = JsonConvert.DeserializeObject<TResponseType>(response.Content);
            }
            catch (Exception exception)
            {
                throw CreateRequestException(
                    response: response,
                    message: "Ошибка десериализации",
                    exception: exception);
            }

            if (deserializedObject is null)
            {
                throw CreateRequestException(
                    response: response,
                    message: "Десериализованный объект ничего в себе не содержит (null)");
            }

            return deserializedObject;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static RequestException CreateRequestException(
            RestResponse response,
            string message,
            Exception? exception = default) =>
            exception is null
                ? new(
                    message: message,
                    httpStatusCode: response.StatusCode
                )
                : new(
                    message: message,
                    httpStatusCode: response.StatusCode,
                    innerException: exception
                );
    }
}
