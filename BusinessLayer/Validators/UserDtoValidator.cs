using BusinessLayer.Dto;

namespace BusinessLayer.Validators
{
    public class UserDtoValidator : BaseValidator<UserDto>
    {
        public UserDtoValidator(UserDto model) : base(model)
        {

        }
    }
}
