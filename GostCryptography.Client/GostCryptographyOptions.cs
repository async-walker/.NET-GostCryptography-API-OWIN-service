namespace GostCryptography.Client
{
    public class GostCryptographyOptions
    {
        public string ApiAddress { get; set; }

        public GostCryptographyOptions(string apiAddress)
        {
            ApiAddress = apiAddress;
        }
    }
}
