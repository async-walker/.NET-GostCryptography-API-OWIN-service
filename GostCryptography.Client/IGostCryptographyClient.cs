using System.Security.Cryptography.X509Certificates;

namespace GostCryptography.Client
{
    public interface IGostCryptographyClient
    {
        Task<byte[]> SignMessageCMS(
            byte[] message,
            X509FindType findType,
            StoreLocation storeLocation,
            StoreName storeName,
            string findValue);
        Task<byte[]> VerifySignCMS(byte[] message);
    }
}
