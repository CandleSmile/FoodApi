namespace Utilities.ErrorHandle
{
    public class BaseException : Exception
    {
        public int ErrorCode { get; set; }

        public BaseException(int errorCode, string error)
            : base(error)
        {
            ErrorCode = errorCode;
        }
    }
}
