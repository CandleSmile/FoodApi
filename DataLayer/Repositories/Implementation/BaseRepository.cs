using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Implementation
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class      
    {
        protected readonly FoodContext _context;
        public BaseRepository(FoodContext context)
        {
            _context = context;
        }
        public async Task<T?> GetByIdAsync(int id)
        {           
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
       
        public async  Task AddAsync(T entity)
        {
           await  _context.Set<T>().AddAsync(entity);
        }
            
        public async Task RemoveAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity is null)
            {
                throw new Exception($"Object with  {id} is not found.");
            }

            _context.Set<T>().Remove(entity);
        }
       
    }
}
