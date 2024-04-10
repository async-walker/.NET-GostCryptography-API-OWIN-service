using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GostCryptographyAPI.Helpers
{
    public static class HttpResponseMessageHelper
    {
        public static HttpResponseMessage GetHttpResponseMessage(
            HttpStatusCode statusCode,
            HttpContent content,
            string contentType)
        {
            var result = new HttpResponseMessage(statusCode)
            {
                Content = content
            };
            result.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);

            return result;
        }
    }
}
