using DataLayer.Items;

namespace DataLayer.Repositories.Interfaces
{
    public interface ITagRepository : IBaseRepository<Tag>
    {
        Task<Tag?> GetTagByNameAsync(string name);
    }
}
