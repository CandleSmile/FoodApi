using System.ComponentModel.DataAnnotations;
using Utilities.Helpers;

namespace BusinessLayer.Validators
{
    public static class Validate
    {
        public static void ValidateClass(object obj)
        {
            var context = new ValidationContext(obj);
            var results = new List<ValidationResult>();
            string listErrors = string.Empty;
            if (!Validator.TryValidateObject(obj, context, results, true))
            {
                foreach (var error in results)
                {
                    listErrors += error.ErrorMessage + ",";
                }

                throw ValidationExceptions.NotValid(listErrors);
            }
        }

        public static void ValidateShouldBeNull(object obj, string? message)
        {
            if (obj != null)
            {
                throw ValidationExceptions.AlreadyExists(message ?? $"The object already exists");
            }
        }

        public static void ValidateIsNotNulll(object obj, string? message)
        {
            if (obj == null)
            {
                throw ValidationExceptions.ObjectNotFound(message ?? $"The object  wasn't found");
            }
        }

        public static void ValidateIsNotNulllOrEmpty(string? str, string? message)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw ValidationExceptions.ObjectNotFound(message ?? $"The string  wasn't found");
            }
        }

        public static void ValidatePassword(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            if (!HashHelper.VerifyPasswordHash(password, passwordSalt, passwordHash))
            {
                throw ValidationExceptions.NotValid("Password was wrong");
            }
        }

        public static void ValidateShouldBeEqual(string obj1, string obj2, string? message, int errorCode)
        {
            if (obj1 != obj2)
            {
                throw ValidationExceptions.NotValidWithErrorCode(message ?? $"The object  wasn't found", errorCode);
            }
        }

        public static void ValidateGreterZero(int number, string? message, int errorCode)
        {
            if (number <= 0)
            {
                throw ValidationExceptions.NotValidWithErrorCode(message ?? $"The list is empty", errorCode);
            }
        }
    }
}
