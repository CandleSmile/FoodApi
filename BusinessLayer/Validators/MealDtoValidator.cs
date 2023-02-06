using BusinessLayer.Dto;
using DataLayer.Repositories.Interfaces;

namespace BusinessLayer.Validators
{
    public class MealDtoValidator : BaseValidator<MealDto>
    {
        private IUnitOfWork _unitOfWork;

        public MealDtoValidator(MealDto model, IUnitOfWork unitOfWork) : base(model)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
