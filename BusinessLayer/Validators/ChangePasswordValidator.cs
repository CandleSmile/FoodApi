using BusinessLayer.Dto;

namespace BusinessLayer.Validators
{
    public class ChangePasswordValidator : BaseValidator<ChangePasswordDto>
    {

        public ChangePasswordValidator(ChangePasswordDto model) : base(model)
        {
        }
    }
}
