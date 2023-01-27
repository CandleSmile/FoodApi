using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.Services.Interfaces;
using DataLayer.Items;
using DataLayer.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
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

        public async Task<UserDto> AddAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            (user.PasswordSalt, user.PasswordHash) = HashHelper.CreatePasswordHash(userDto.Password);
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveAsync();
            return _mapper.Map <UserDto>(user);
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
     
    }
}
