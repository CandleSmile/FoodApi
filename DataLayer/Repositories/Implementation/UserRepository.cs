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
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        public UserRepository(FoodContext context) : base(context)
        {
        }

        public async Task<User?> GetUserByNameAsync(string username)
        {
            return await _context.Users.FirstAsync(user=> user.Username == username);    
        }
    }
}
