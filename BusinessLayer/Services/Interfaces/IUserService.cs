using BusinessLayer.Contracts;

namespace BusinessLayer.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetUsers();
    }
}
