using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Blog.Domain.Contracts;
using Blog.Domain.Interfaces;
using Blog.Domain.Models;

namespace Blog.Data.Repositories
{
    internal class Repository<T> : BaseRepository, IRepository<T> where T : class
    {
        private readonly DbSet<T> _entitySet;
        private readonly IQueryable<T> _entities;

        public Repository(DbContext context, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _entitySet = context.Set<T>();
            _entities = ((BlogContext)context).GetEntities<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _entities;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> where)
        {
            return _entities.Where(where);
        }

        public void Add(T entity)
        {
            _entitySet.Add(entity);
            UnitOfWork.SaveChanges();
        }

        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            UnitOfWork.SaveChanges();
        }

        public void Attach(T entity)
        {
            _entitySet.Attach(entity);
            UnitOfWork.SaveChanges();
        }

        public void Delete(T entity)
        {
            var deletable = typeof(T).GetInterface("IDeletableEntity");
            if (deletable == null)
                throw new BusinessServicesException(String.Format("'{0}' is not deletable.", typeof(T)));
            _entitySet.Remove(entity);
            UnitOfWork.SaveChanges();
        }

        public void Delete(object id)
        {
            var deletable = typeof(T).GetInterface("IDeletableEntity");
            if (deletable == null)
                throw new BusinessServicesException(String.Format("'{0}' is not deletable.", typeof(T)));
            var entityToDelete = _entitySet.Find(id);
            Delete(entityToDelete);
        }

        public void Archive(T entity)
        {
            var archivable = typeof(T).GetInterface("IArchivableEntity");
            if (archivable == null)
                throw new BusinessServicesException(String.Format("'{0}' is not archivable.", typeof(T)));
            var property = typeof(T).GetProperty("State");
            if (property != null && property.CanWrite)
            {
                property.SetValue(entity, State.Archived);
            }
            UnitOfWork.SaveChanges();
        }

        public void SetActive(T entity)
        {
            var property = typeof(T).GetProperty("State");
            if (property != null && property.CanWrite)
            {
                property.SetValue(entity, State.Active);
            }
            UnitOfWork.SaveChanges();
        }

        public void SetInActive(T entity)
        {
            var property = typeof(T).GetProperty("State");
            if (property != null && property.CanWrite)
            {
                property.SetValue(entity, State.InActive);
            }
            UnitOfWork.SaveChanges();
        }
    }  
}
