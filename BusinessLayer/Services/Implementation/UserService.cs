using AutoMapper;
using BusinessLayer.Dto;
using BusinessLayer.Services.Interfaces;
using DataLayer.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }



        public async Task<UserDto?> GetUserByNameAsync(string userName)
        {
            var user = await _unitOfWork.Users.GetUserByNameAsync(userName);
            return user != null ? _mapper.Map<UserDto>(user) : null;
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            return _mapper.Map<IEnumerable<UserDto>>(await _unitOfWork.Users.GetAllAsync());
        }
    }
}
