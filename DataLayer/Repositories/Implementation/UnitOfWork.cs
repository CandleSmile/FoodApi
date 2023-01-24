using DataLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FoodContext _context;
        public UnitOfWork(FoodContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            
        }
        public IUserRepository Users { get; private set; }
        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
