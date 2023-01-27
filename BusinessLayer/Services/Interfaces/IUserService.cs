using BusinessLayer.Contracts;
using System.Security.Claims;

namespace BusinessLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsersAsync();
        Task<UserDto> AddAsync(UserDto userDto);
        Task<UserDto?> GetUserByNameAsync(string userName);
       
    }
}
