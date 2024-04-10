using System.Security.Cryptography.X509Certificates;

namespace GostCryptography.Client.Tests
{
    public class GostCryptographyClientTests
    {
        IGostCryptographyClient _client;

        public GostCryptographyClientTests()
        {
            _client = new GostCryptographyClient(
                new GostCryptographyOptions("http://localhost:5000/api/"));
        }

        [Fact]
        public async void SignMessageCMSTest()
        {
            var signedData = await _client.SignMessageCMS(
                message: File.ReadAllBytes("N:\\«¿ƒ¿◊»\\¡ »\\Œ ¡\\—œœ\\7f4d589b-2b65-4d29-840e-0f728f99609b\\qcb_request.xml"),
                signerSubjectName: "»‚‡ÌÓ‚‡",
                storeLocation: StoreLocation.CurrentUser,
                storeName: StoreName.My);

            Assert.NotNull(signedData);
        }

        [Fact]
        public async void VerifySignCMSTest()
        {
            var signedData = await _client.VerifySignCMS(
                message: File.ReadAllBytes("N:\\«¿ƒ¿◊»\\¡ »\\Œ ¡\\—œœ\\7f4d589b-2b65-4d29-840e-0f728f99609b\\qcb_answer.xml.p7s"));

            Assert.NotNull(signedData);
        }
    }
}