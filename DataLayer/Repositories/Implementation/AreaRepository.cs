using DataLayer.Items;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Implementation
{
    public class AreaRepository : BaseRepository<Area>, IAreaRepository
    {
        public AreaRepository(FoodContext context) : base(context)
        {
        }

        public async Task<Area?> GetAreaByNameAsync(string name)
        {
            return await _context.Areas.FirstOrDefaultAsync(ar => ar.Name == name);
        }
    }
}
