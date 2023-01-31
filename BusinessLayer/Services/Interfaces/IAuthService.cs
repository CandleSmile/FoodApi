using BusinessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> VerifyPasswordHash(string password, int userId);
        string CreateAccessToken(string userName);

        Task<string> CreateRefreshToken(int userId);
        Task<string> RefreshToken(string accessToken, string refreshToken);
        void SetTokensToCookies(string accessToken, string refreshToken);
        void DeleteTokensFromCookies();
        Task<bool> DeleteRefreshTokenFromDb();
        Task<bool> CheckIfLoginned(string userName);

    }
}
