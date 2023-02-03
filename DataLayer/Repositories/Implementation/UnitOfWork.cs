using DataLayer.Repositories.Interfaces;

namespace DataLayer.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FoodContext _context;

        public UnitOfWork(FoodContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Meals = new MealRepository(_context);
            Categories = new CategoryRepository(_context);
            Ingredients = new IngredientRepository(_context);
            Tags = new TagRepository(_context);
            Areas = new AreaRepository(_context);
            Orders = new OrderRepository(_context);
        }

        public IUserRepository Users { get; private set; }

        public IMealRepository Meals { get; private set; }

        public IIngredientRepository Ingredients { get; private set; }

        public ITagRepository Tags { get; private set; }

        public ICategoryRepository Categories { get; private set; }

        public IAreaRepository Areas { get; private set; }

        public IOrderRepository Orders { get; private set; }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
