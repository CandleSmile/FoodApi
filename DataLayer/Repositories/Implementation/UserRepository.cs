using DataLayer.Items;
using DataLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Implementation
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        public UserRepository(FoodContext context) : base(context)
        {
        }
    }
}
