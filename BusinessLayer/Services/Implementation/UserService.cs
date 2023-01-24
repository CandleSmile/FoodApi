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

        public string CreateToken(UserDto user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
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
                expires: DateTime.UtcNow.AddDays(1), 
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
    }
}
