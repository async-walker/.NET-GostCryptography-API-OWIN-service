namespace GostCryptographyAPI.Exceptions
{
    internal class ResponseError
    {
        public ResponseError(int statusCode, string code, string message)
        {
            StatusCode = statusCode;
            Code = code;
            Message = message;
        }

        public int StatusCode { get; private set; }
        public string Code { get; private set; }
        public string Message { get; private set; }
    }
}
