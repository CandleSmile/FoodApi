namespace Utilities.ErrorHandle
{
    public class Error
    {
        public Error(int errorCode, string message = "")
        {
            ErrorCode = errorCode;
            Message = message;
        }

        public int ErrorCode { get; set; }

        public string? Message { get; set; }
    }
}
