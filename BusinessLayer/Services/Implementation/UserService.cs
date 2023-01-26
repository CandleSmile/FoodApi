using AutoMapper;
using Azure.Core;
using BusinessLayer.Contracts;
using BusinessLayer.Services.Interfaces;
using DataLayer.Items;
using DataLayer.Repositories.Interfaces;
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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public UserService(IUnitOfWork unitOfWork,  IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;   
        }

        public async Task AddAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            (user.PasswordSalt, user.PasswordHash) = HashHelper.CreatePasswordHash(userDto.Password);
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task<UserDto?> GetUserByNameAsync(string userName)
        {
            var user = await _unitOfWork.Users.GetUserByNameAsync(userName);
            return user != null ? _mapper.Map <UserDto>(user) : null;
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            return _mapper.Map<IEnumerable<UserDto>>(await _unitOfWork.Users.GetAllAsync());
        }

        public string CreateToken(string userName)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName)
            };
            string? tokenKey = _configuration.GetSection("Jwt")["Key"];
            string? issuer = _configuration.GetSection("Jwt")["Issuer"];
            string? audience = _configuration.GetSection("Jwt")["Audience"];

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
    
        public async Task<bool> VerifyPasswordHash (string password ,UserDto userDto)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userDto.Id);
            if (user == null) { return false; }
            return  HashHelper.VerifyPasswordHash(password, user.PasswordSalt, user.PasswordHash);
        }

        public async Task<(string, string)> RefreshToken(string token, string refreshToken)
        {
            var principal = GetPrincipalFromExpiredToken(token);
            var username = principal.Identity.Name;
            var user = await _unitOfWork.Users.GetUserByNameAsync(username);
            var savedRefreshToken = user?.RefreshToken; 
            if (user == null || savedRefreshToken != refreshToken)
                throw new SecurityTokenException("Invalid refresh token");

            var newJwtToken = CreateToken(username);
            var newRefreshToken = GenerateRefreshToken();
            user.Username = newRefreshToken;
            await _unitOfWork.SaveAsync();

            return (newJwtToken, newRefreshToken);
         
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            string? tokenKey = _configuration.GetSection("Jwt")["Key"];
            string? issuer = _configuration.GetSection("Jwt")["Issuer"];
            string? audience = _configuration.GetSection("Jwt")["Audience"];
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
        public async Task<string> CreateRefreshToken(UserDto userDto)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userDto.Id);
            if (user == null) 
                throw new Exception("the user wasn't found");
            user.RefreshToken = GenerateRefreshToken();
            await  _unitOfWork.SaveAsync();
            return user.RefreshToken;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
