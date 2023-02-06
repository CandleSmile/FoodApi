using AutoMapper;
using BusinessLayer.Dto;
using BusinessLayer.Services.Interfaces;
using DataLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
