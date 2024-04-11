using GostCryptography.Client.Extensions;
using RestSharp;
using System.Security.Cryptography.X509Certificates;

namespace GostCryptography.Client
{
    public class GostCryptographyClient : IGostCryptographyClient, IDisposable
    {
        private readonly RestClient _client;

        public GostCryptographyClient(GostCryptographyOptions options) 
        {
            _client = new RestClient(
                new RestClientOptions(options.ApiAddress));
        }

        /// <inheritdoc/>   
        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<byte[]> SignMessageCMS(
            byte[] message, 
            X509FindType findType,
            StoreLocation storeLocation,
            StoreName storeName,
            string findValue)
        {
            var request = new RestRequest("CMS/SignMessage", Method.Post)
                .AddBody(message, ContentType.Binary)
                .AddQueryParameter("storeLocation", storeLocation)
                .AddQueryParameter("storeName", storeName)
                .AddQueryParameter("findType", findType)
                .AddQueryParameter("findValue", findValue);

            var response = await _client.GetResponseAsync(request);

            return response.RawBytes!;
        }

        public async Task<byte[]> VerifySignCMS(byte[] message)
        {
            var request = new RestRequest("CMS/VerifySign", Method.Post)
                .AddBody(message, ContentType.Binary);

            var response = await _client.GetResponseAsync(request);

            return response.RawBytes!;
        }
    }
}
