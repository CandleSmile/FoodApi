using System.ComponentModel.DataAnnotations;

namespace Utilities.ErrorHandle
{
    public class ModelNotValidException : ApplicationException
    {
        public string Error { get; set; }

        public ModelNotValidException(ValidationResult result) 
        {
            Error = result.ErrorMessage;
        }
    }
}
