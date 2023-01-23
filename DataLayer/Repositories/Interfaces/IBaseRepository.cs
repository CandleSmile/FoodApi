﻿using System.Linq.Expressions;


namespace DataLayer.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T? GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);       
        void Remove(T entity);
    }
}