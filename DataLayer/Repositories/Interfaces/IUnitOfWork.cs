namespace DataLayer.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }

        IMealRepository Meals { get; }

        IIngredientRepository Ingredients { get; }

        ITagRepository Tags { get; }

        ICategoryRepository Categories { get; }

        IAreaRepository Areas { get; }

        IOrderRepository Orders { get; }

        IDeliveryDateTimeSlotRepository DeliveryDateTimeSlots { get; }

        Task<int> SaveAsync();
    }
}
