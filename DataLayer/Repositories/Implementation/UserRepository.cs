using DataLayer.Items;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementation
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(FoodContext context) : base(context)
        {
        }

        public async Task<User?> GetUserByNameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Username == username);
        }

        public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.RefreshToken == refreshToken);
        }
    }
}
