using System.Linq.Expressions;
using System.Threading.Tasks;


namespace DataLayer.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();

        Task AddAsync(T entity);
        Task RemoveAsync(int id);
    }
}
