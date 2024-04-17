using System;
using System.Security.Cryptography.X509Certificates;

namespace GostCryptographyAPI.Exceptions
{
    [Serializable]
    internal class CertificateNotFoundException : Exception
    {
        public CertificateNotFoundException(
            StoreLocation storeLocation,
            StoreName storeName,
            X509FindType findType,
            string findValue) 
            : base(
                GetMessageEx(storeLocation, storeName, findType, findValue))
        {
            StoreLocation = storeLocation;
            StoreName = storeName;
            FindType = findType;
        }

        public StoreLocation StoreLocation { get; set; }
        public StoreName StoreName { get; set; }
        public X509FindType FindType { get; set; }
        public string FindValue { get; set; }

        private static string GetMessageEx(
            StoreLocation storeLocation,
            StoreName storeName,
            X509FindType findType,
            string findValue)
        {
            var message =
                $"Сертификат с поиском по {findType} и значением [{findValue}] " +
                $"не был найден в хранилище {storeLocation}/{storeName}";

            return message;
        }
    }
}
