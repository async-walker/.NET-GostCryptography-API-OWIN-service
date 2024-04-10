using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        message: $"Ответ неуспешен ({response.StatusCode})");
            }

            return response;
        }
    }
}
