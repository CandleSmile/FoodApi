using BusinessLayer.Contracts;

namespace BusinessLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsersAsync();
        Task AddAsync(UserDto userDto);
        Task<UserDto?> GetUserByNameAsync(string userName);
        string CreateToken(UserDto user);
        Task<bool> VerifyPasswordHash(string password, UserDto userDto);
    }
}
