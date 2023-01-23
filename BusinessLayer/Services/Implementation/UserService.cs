using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.Services.Interfaces;
using DataLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork,  IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<UserDto> GetUsers()
        {
            return _mapper.Map<IEnumerable<UserDto>>(_unitOfWork.Users.GetAll());
        }
    }
}
