using DataLayer.Items;

namespace DataLayer.Repositories.Interfaces
{
    public interface IAreaRepository : IBaseRepository<Area>
    {
        Task<Area?> GetAreaByNameAsync(string name);
    }
}
