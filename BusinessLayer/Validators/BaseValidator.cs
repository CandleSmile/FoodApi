namespace BusinessLayer.Validators
{
    public class BaseValidator<T> where T : class
    {
        protected T _model;

        public BaseValidator(T model) { _model = model; }

        public virtual void Valid()
        {
            Validate.ValidateClass(_model);
        }
    }
}
