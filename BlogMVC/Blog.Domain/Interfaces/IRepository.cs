using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Blog.Domain.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> where);

        // other data access methods could also be included.

        void Add(T entity);
        void Update(T entity);
        void Attach(T entity);
        void Delete(T entity);
        void Delete(object id);
    }
}
