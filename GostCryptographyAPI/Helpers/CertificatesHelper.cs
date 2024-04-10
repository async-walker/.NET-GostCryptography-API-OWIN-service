using GostCryptographyAPI.Exceptions;
using Serilog;
using System.Security.Cryptography.X509Certificates;

namespace GostCryptographyAPI.Helpers
{
    public static class CertificatesHelper
    {
        public static X509Certificate2 FindCertificateBySubject(
            string subjectName,
            StoreLocation storeLocation,
            StoreName storeName)
        {
            using (var store = new X509Store(storeName, storeLocation))
            {
                store.Open(OpenFlags.ReadOnly);

                var certificates = store.Certificates.Find(
                    findType: X509FindType.FindBySubjectName,
                    findValue: subjectName,
                    validOnly: true);

                if (certificates.Count > 0)
                {
                    Log.Information("Найдено {0} сертификатов в хранилище для субъекта {1}", certificates.Count, subjectName);

                    return certificates[0];
                }
                else throw new CertificateNotFoundException(
                    $"Сертификат субъекта [{subjectName}] не был найден в хранилище");
            }
        }
    }
}
