using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.Services.Interfaces;
using DataLayer.Items;
using DataLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Utilities.Helpers;

namespace BusinessLayer.Services.Implementation
{
    public class AuthService:IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> VerifyPasswordHash(string password, int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null) { 
                throw new Exception("the user wasn't found");
            }

            return HashHelper.VerifyPasswordHash(password, user.PasswordSalt, user.PasswordHash);
        }
        public string CreateAccessToken(string userName)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName)
            };
            (string? tokenKey, string? issuer, string? audience) = GetConfigurationOfTokens();
            if (String.IsNullOrEmpty(tokenKey) || String.IsNullOrEmpty(issuer) || String.IsNullOrEmpty(audience)) {
                throw new Exception("the configuration is empty");
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenKey ?? ""));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: cred
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        
        public async Task<string> CreateRefreshToken(int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("the user wasn't found");
            user.RefreshToken = RandomBase64String();
            await _unitOfWork.SaveAsync();
            return user.RefreshToken;
        }

        public async Task<(string, string)> RefreshTokens(string accessToken, string refreshToken)
        {
            var user = await GetUserByToken(accessToken);
            if (user == null)
                throw new Exception("the user wasn't found");
           
            var savedRefreshToken = user?.RefreshToken;
            if ( savedRefreshToken != refreshToken)
                throw new SecurityTokenException("Invalid refresh token");
            var newAccessToken = CreateAccessToken(user.Username);

            user.RefreshToken = RandomBase64String();
            await _unitOfWork.SaveAsync();
            return (newAccessToken,user.RefreshToken);

        }
       
        public void SetTokensToCookies(string accessToken, string refreshToken)
        {
            var ctx = _httpContextAccessor.HttpContext;
            ctx.Response.Cookies.Append(_configuration.GetSection("CookiesKeys")["AccessTokenKey"], accessToken,
                       new CookieOptions
                       {
                           MaxAge = TimeSpan.FromMinutes(60)
                       });
            ctx.Response.Cookies.Append(_configuration.GetSection("CookiesKeys")["RefreshTokenKey"], refreshToken,
                new CookieOptions
                {
                    MaxAge = TimeSpan.FromDays(1)
                });
            ctx.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            ctx.Response.Headers.Add("X-Xss-Protection", "1");
            ctx.Response.Headers.Add("X-Frame-Options", "DENY");
        }

        public void DeleteTokensFromCookies()
        {
            var ctx = _httpContextAccessor.HttpContext;
            if (ctx.Request.Cookies[_configuration.GetSection("CookiesKeys")["AccessTokenKey"]] != null)
                ctx.Response.Cookies.Delete(_configuration.GetSection("CookiesKeys")["AccessTokenKey"]);

            if (ctx.Request.Cookies[_configuration.GetSection("CookiesKeys")["RefreshTokenKey"]] != null)
                ctx.Response.Cookies.Delete(_configuration.GetSection("CookiesKeys")["RefreshTokenKey"]);

        }
        
        //private methods
        private async Task<User?> GetUserByToken(string token)
        {
            var principal = GetPrincipalFromExpiredToken(token);
            if (principal == null)
                throw new Exception("the principal wasn't found");

            var username = principal.Identity.Name;
            return await _unitOfWork.Users.GetUserByNameAsync(username);
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            (string? tokenKey, string? issuer, string? audience) = GetConfigurationOfTokens();
            if (String.IsNullOrEmpty(tokenKey) || String.IsNullOrEmpty(issuer) || String.IsNullOrEmpty(audience))
            {
                throw new Exception("the configuration is empty");
            }

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateLifetime = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey ?? "")),
                ValidateIssuerSigningKey = true,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
        private string RandomBase64String()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
       
        private (string?, string?, string) GetConfigurationOfTokens()
        {
            return (_configuration.GetSection("Jwt")["Key"], _configuration.GetSection("Jwt")["Issuer"], _configuration.GetSection("Jwt")["Audience"]);

        }

       

        
    }
}
