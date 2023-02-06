using BusinessLayer.Dto;

namespace BusinessLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsersAsync();

        Task<UserDto?> GetUserByNameAsync(string userName);
    }
}
