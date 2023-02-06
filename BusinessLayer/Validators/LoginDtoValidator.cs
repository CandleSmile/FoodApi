using FoodApi.Models;

namespace BusinessLayer.Validators
{
    public class LoginDtoValidator : BaseValidator<LoginDto>
    {
        public LoginDtoValidator(LoginDto model) : base(model)
        {
        }

        public void ValidatePassword(byte[] passwordSalt, byte[] passwordHash)
        {
            Validate.ValidatePassword(_model.Password, passwordSalt, passwordHash);
        }
    }
}
