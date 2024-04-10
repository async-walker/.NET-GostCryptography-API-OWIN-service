using System.Security.Cryptography.X509Certificates;

namespace GostCryptography.Client
{
    public interface IGostCryptographyClient
    {
        Task<byte[]> SignMessageCMS(
            byte[] message,
            string signerSubjectName,
            StoreLocation storeLocation,
            StoreName storeName);
        Task<byte[]> VerifySignCMS(byte[] message);
    }
}
