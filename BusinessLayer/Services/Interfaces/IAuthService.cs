using BusinessLayer.Dto;
using FoodApi.Models;

namespace BusinessLayer.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> RefreshToken(string accessToken, string refreshToken);

        void DeleteTokensFromCookies();

        Task<bool> DeleteRefreshTokenFromDb();

        Task<bool> CheckIfLoginned(string userName);

        Task<bool> LoginUserAsync(LoginDto login);

        Task<UserDto> RegisterAsync(RegistrationDto userDto);

        Task ChangePasswordAsync(ChangePasswordDto changePassword, int userId);
    }
}
