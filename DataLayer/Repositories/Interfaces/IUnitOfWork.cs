using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Task<int> SaveAsync();
    }
}
