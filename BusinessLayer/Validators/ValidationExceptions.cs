using Utilities.ErrorHandle;

namespace BusinessLayer.Validators
{
    public static class ValidationExceptions
    {
        public static BadRequestExeption NotValid(string message)
        {
            return new BadRequestExeption((int)ErrorCodes.NoValidData, message);
        }

        public static BadRequestExeption NotValidWithErrorCode(string message, int errorCode)
        {
            return new BadRequestExeption(errorCode, message);
        }

        public static BadRequestExeption AlreadyExists(string message)
        {
            return new BadRequestExeption((int)ErrorCodes.ObjectAlreadyExists, message);
        }

        public static NotFoundException ObjectNotFound(string message)
        {
            return new NotFoundException("The User wasn't found");
        }
    }
}
