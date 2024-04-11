using RestSharp;

namespace GostCryptography.Client.Extensions
{
    internal static class RestClientExtensions
    {
        public static async Task<RestResponse> GetResponseAsync(
           this RestClient client,
           RestRequest request)
        {
            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                if (response.RawBytes is not null)
                    return response;
                else if (response.Content is not null)
                    return response;
                else
                    throw new ArgumentNullException(
                        paramName: nameof(response.Content),
                        message: 
                        $"Контент ответа отсутствует\n" +
                        $" (url запроса: [{response.ResponseUri}])");
            }

            return response;
        }
    }
}
