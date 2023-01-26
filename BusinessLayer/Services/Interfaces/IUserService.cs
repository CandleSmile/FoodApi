using BusinessLayer.Contracts;
using System.Security.Claims;

namespace BusinessLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsersAsync();
        Task AddAsync(UserDto userDto);
        Task<UserDto?> GetUserByNameAsync(string userName);
        string CreateToken(string userName);
        Task<bool> VerifyPasswordHash(string password, UserDto userDto);

        Task<(string, string)> RefreshToken(string token, string refreshToken);
        Task<string> CreateRefreshToken(UserDto userDto);
    }
}
