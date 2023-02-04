namespace Utilities.ErrorHandle
{
    public enum ErrorCodes
    {
        ObjectNotFound = 1,
        ObjectAlreadyExists = 2,
        NoValidData = 3,
        NotValidRefreshToken = 4,
        UnexpectedError = 5,
        TokenWasRefreshed = 6,
        ErrorOnRefreshToken = 7,
        LoginExpired = 8
    };
}
