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
            string signerSubjectName,
            StoreLocation storeLocation,
            StoreName storeName)
        {
            var request = new RestRequest("CMS/SignMessage", Method.Post)
                .AddBody(message, ContentType.Binary)
                .AddQueryParameter("subjectName", signerSubjectName)
                .AddQueryParameter("storeLocation", storeLocation)
                .AddQueryParameter("storeName", storeName);

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
