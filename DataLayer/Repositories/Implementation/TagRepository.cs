using DataLayer.Items;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementation
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(FoodContext context) : base(context)
        {
        }

        public async Task<Tag?> GetTagByNameAsync(string name)
        {
            return await _context.Tags.FirstOrDefaultAsync(tag => tag.Name == name);
        }
    }
}
