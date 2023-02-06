using DataLayer.Items;

namespace DataLayer.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetUserByNameAsync(string username);
        Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
    }
}
