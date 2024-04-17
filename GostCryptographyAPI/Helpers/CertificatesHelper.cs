using GostCryptographyAPI.Exceptions;
using Serilog;
using System.Security.Cryptography.X509Certificates;

namespace GostCryptographyAPI.Helpers
{
    public static class CertificatesHelper
    {
        public static X509Certificate2 FindCertificate(
            StoreLocation storeLocation,
            StoreName storeName,
            X509FindType findType,
            string findValue)
        {
            Log.Information("Начат процесс поиска сертификатов в хранилище");

            using (var store = new X509Store(storeName, storeLocation))
            {
                store.Open(OpenFlags.ReadOnly);

                var certificates = store.Certificates.Find(
                    findType: findType,
                    findValue: findValue,
                    validOnly: true);

                if (certificates.Count > 0)
                {
                    var targetCert = certificates[0];

                    Log.Information(
                        "Найдено {0} сертификатов с поиском по {1} и значением [{2}] в хранилище {3}/{4}.\n" +
                        "Будет использован следующий сертификат: {5}", 
                        certificates.Count, findType, findValue, storeLocation, storeName, targetCert.Subject);

                    return targetCert;
                }
                else
                {
                    Log.Error(
                        "Найдено {0} валидных сертификатов в хранилище {1}/{2} с поиском по {3} ({4})",
                        0, storeLocation, storeName, findType, findValue);

                    throw new CertificateNotFoundException(storeLocation, storeName, findType, findValue);
                }
            }
        }
    }
}
