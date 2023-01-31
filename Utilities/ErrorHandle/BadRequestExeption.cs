namespace Utilities.ErrorHandle
{
    public class BadRequestExeption : ApplicationException
    {
        public BadRequestExeption(string error) : base(error)
        {

        }
    }
}
