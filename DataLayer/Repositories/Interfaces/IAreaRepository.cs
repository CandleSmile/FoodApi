using DataLayer.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Interfaces
{
    public interface IAreaRepository:IBaseRepository<Area>
    {
        Task<Area?> GetAreaByNameAsync(string name);
    }
}
