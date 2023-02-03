namespace Utilities.ErrorHandle
{
    public class BadRequestExeption : BaseException
    {

        public BadRequestExeption(int errorCode, string error)
            : base(errorCode, error)
        {
        }
    }
}
