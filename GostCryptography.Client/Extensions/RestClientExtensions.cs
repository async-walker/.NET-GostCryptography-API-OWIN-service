using GostCryptography.Client.Exceptions;
using RestSharp;

namespace GostCryptography.Client.Extensions
{
    internal static class RestClientExtensions
    {
        public static async Task<RestResponse> GetResponseAsync(
           this IRestClient client,
           RestRequest request,
           IExceptionParser exceptionsParser)
        {
            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var failedApiResponse = response.DeserializeContent<ApiResponseError>();

                throw exceptionsParser.Parse(failedApiResponse);
            }

            return response;
        }
    }
}
