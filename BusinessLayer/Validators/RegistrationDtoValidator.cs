using BusinessLayer.Dto;
using DataLayer.Repositories.Interfaces;

namespace BusinessLayer.Validators
{
    public class RegistrationDtoValidator : BaseValidator<RegistrationDto>
    {
        private IUnitOfWork _unitOfWork;

        public RegistrationDtoValidator(RegistrationDto model, IUnitOfWork unitOfWork) : base(model)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ValidateModel()
        {
            base.Valid();
            var user = await _unitOfWork.Users.GetUserByNameAsync(_model.Username);
            Validate.ValidateShouldBeNull(user, "The user exists.");
        }
    }
}
