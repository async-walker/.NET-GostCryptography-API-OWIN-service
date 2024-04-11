using GostCryptography.Pkcs;
using GostCryptographyAPI.Helpers;
using GostCryptographyAPI.Types;
using Serilog;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;

namespace GostCryptographyAPI.Controllers
{
    public class CMSController : ApiController
    {
        private readonly ILogger _logger;

        public CMSController()
        {
            _logger = Log.Logger;
        }

        [HttpGet]
        public string Get()
        {
            return 
                "Available CMS methods: \n" +
                "1)SignMessage \n" +
                "2)VerifySign \n";
        }

        [HttpPost]
        public HttpResponseMessage SignMessage(
            [FromBody] byte[] message,
            [FromUri] string certFindValue,
            [FromUri] StoreLocation storeLocation = StoreLocation.CurrentUser,
            [FromUri] StoreName storeName = StoreName.My,
            [FromUri] X509FindType findType = X509FindType.FindBySubjectName)
        {
            try
            {
                var signerCert = CertificatesHelper.FindCertificate(
                    storeLocation, storeName, findType, certFindValue);

                var signedMessage = GostCryptographyCMSHelper.SignMessage(signerCert, message);

                return HttpResponseMessageHelper.GetHttpResponseMessage(
                    statusCode: HttpStatusCode.OK,
                    content: new ByteArrayContent(signedMessage),
                    contentType: ContentType.Binary);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Исключение при подписи сообщения");

                return HttpResponseMessageHelper.GetHttpResponseMessage(
                    statusCode: HttpStatusCode.InternalServerError,
                    content: new StringContent(
                        $"Сообщение об ошибке: {ex.Message}\n\n" +
                        $"Предшествующая ошибка: {ex.InnerException.Message}"),
                    contentType: ContentType.Text);
            }
        }

        [HttpPost]
        public HttpResponseMessage VerifySign([FromBody] byte[] signedMessage)
        {
            try
            {
                var signedCms = new GostSignedCms();
                signedCms.Decode(signedMessage);

                try
                {
                    signedCms.CheckSignature(true);

                    return HttpResponseMessageHelper.GetHttpResponseMessage(
                        statusCode: HttpStatusCode.OK,
                        content: new ByteArrayContent(signedCms.ContentInfo.Content),
                        contentType: ContentType.Binary);
                }
                catch
                {
                    _logger.Information("Подпись сообщения невалидна");

                    return HttpResponseMessageHelper.GetHttpResponseMessage(
                        statusCode: HttpStatusCode.OK,
                        content: new StringContent("Подпись сообщения невалидна"),
                        contentType: ContentType.Text);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Исключение при проверке подписи");

                return HttpResponseMessageHelper.GetHttpResponseMessage(
                    statusCode: HttpStatusCode.InternalServerError,
                    content: new StringContent(ex.Message),
                    contentType: ContentType.Text);
            }
        }
    }
}
