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
                    Log.Information(
                        "Найдено {0} сертификатов " +
                        "с поиском по {1} значением [{2}] в хранилище {3}/{4}", 
                        certificates.Count, findType, findValue, storeLocation, storeName);

                    return certificates[0];
                }
                else
                {
                    Log.Error(
                        "Найдено {0} валидных сертификатов в хранилище {1}/{2} с поиском по {3} ({4})",
                        0, storeLocation, storeName, findType, findValue);

                    throw new CertificateNotFoundException(
                        $"Сертификат субъекта [{findValue}] не был найден в хранилище");
                }
            }
        }
    }
}
