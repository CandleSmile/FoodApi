namespace Utilities.ErrorHandle
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base((int)ErrorCodes.ObjectNotFound, message)
        {
        }
    }
}
