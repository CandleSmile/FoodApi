namespace BusinessLayer.Validators
{
    public class InputValidator
    {
        public void ValidateIsNotNull(object obj, string? message)
        {
            Validate.ValidateIsNotNulll(obj, message);
        }

        public void ValidateShouldBeNull(object obj, string? message)
        {
            Validate.ValidateShouldBeNull(obj, message);
        }

        public void ValidateShouldBeEqual(string obj1, string obj2, string? message, int errorCode)
        {
            Validate.ValidateShouldBeEqual(obj1, obj2, message, errorCode);
        }

        public void ValidateIsNotNullOrEmpty(string? str, string? message)
        {
            Validate.ValidateIsNotNulllOrEmpty(str, message);
        }
    }
}
