using DataLayer.Repositories.Interfaces;
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
        public T? GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }
            
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
       
    }
}
