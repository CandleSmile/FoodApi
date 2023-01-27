using BusinessLayer.Contracts.DBLoad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Interfaces
{
    public interface IDbLoaderService
    {
        Task<string> LoadDb(DbLoadModel model);
    }
}
