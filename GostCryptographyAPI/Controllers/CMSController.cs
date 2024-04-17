using GostCryptography.Pkcs;
using GostCryptographyAPI.Exceptions;
using GostCryptographyAPI.Helpers;
using GostCryptographyAPI.Types;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
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
                "Доступные методы CMS:\n" +
                "1)SignMessage\n" +
                "2)VerifySign\n";
        }

        [HttpPost]
        public HttpResponseMessage SignMessage(
            [FromBody] byte[] message,
            [FromUri] string findValue,
            [FromUri] StoreLocation storeLocation = StoreLocation.CurrentUser,
            [FromUri] StoreName storeName = StoreName.My,
            [FromUri] X509FindType findType = X509FindType.FindBySubjectName)
        {
            if (message == null || message.Length < 1)
            {
                return HttpResponseMessageHelper.GetHttpResponseMessage(
                    statusCode: HttpStatusCode.BadRequest,
                    content: new StringContent(
                        "Тело запроса должно содержать параметр [message] и не быть пустым"),
                    contentType: ContentType.Text);
            }

            try
            {
                var signerCert = CertificatesHelper.FindCertificate(
                    storeLocation, storeName, findType, findValue);

                var signedMessage = GostCryptographyCMSHelper.SignMessage(signerCert, message);

                return HttpResponseMessageHelper.GetHttpResponseMessage(
                    statusCode: HttpStatusCode.OK,
                    content: new ByteArrayContent(signedMessage),
                    contentType: ContentType.Binary);
            }
            catch (CertificateNotFoundException ex)
            {
                var responseError = new ResponseError(
                    statusCode: (int)HttpStatusCode.BadRequest,
                    code: ResponseCodes.CERT_NOT_FOUND,
                    message: ex.Message);

                return HttpResponseMessageHelper.GetHttpResponseMessage(
                    statusCode: HttpStatusCode.BadRequest,
                    content: new StringContent(
                        JsonConvert.SerializeObject(responseError)),
                    contentType: ContentType.Text);
            }
            catch (CryptographicException ex)
            {
                var responseError = new ResponseError(
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    code: ResponseCodes.CRYPTOGRAPHIC_EXCEPTION,
                    message: ex.Message);

                return HttpResponseMessageHelper.GetHttpResponseMessage(
                    statusCode: HttpStatusCode.InternalServerError,
                    content: new StringContent(
                        JsonConvert.SerializeObject(responseError)),
                    contentType: ContentType.Text);
            }
            catch (Exception ex)
            {
                var responseError = new ResponseError(
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    code: ResponseCodes.OTHER_EXCEPTION,
                    message: ex.Message);

                return HttpResponseMessageHelper.GetHttpResponseMessage(
                    statusCode: HttpStatusCode.InternalServerError,
                     content: new StringContent(
                        JsonConvert.SerializeObject(responseError)),
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
