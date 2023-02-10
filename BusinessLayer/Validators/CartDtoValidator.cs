using BusinessLayer.Dto;
using DataLayer.Repositories.Interfaces;
using Utilities.ErrorHandle;

namespace BusinessLayer.Validators
{
    public class CartDtoValidator : BaseValidator<CartDto>
    {
        private IUnitOfWork _unitOfWork;

        public CartDtoValidator(CartDto model) : base(model)
        {
        }

        public override void Valid()
        {
            base.Valid();
            Validate.ValidateGreterZero(_model.CartItems.Count, "The list of cart items can't be empty", (int)ErrorCodes.NoValidData);
        }
    }
}
