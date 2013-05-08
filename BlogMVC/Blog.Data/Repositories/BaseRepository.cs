using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using Blog.Domain.Contracts;
using Blog.Domain.Interfaces;
using Blog.Domain.Models;

namespace Blog.Data.Repositories
{
    public class BaseRepository
    {
        protected IUnitOfWork UnitOfWork { get; set; }
        protected static List<Type> Roots { get; set; }
        protected List<Type> Children { get; set; }

        internal BlogContext Context
        {
            get { return (BlogContext)UnitOfWork; }
        }

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            UnitOfWork = unitOfWork;
            Roots = new List<Type>
                {
                    typeof (Category),
                    typeof (Contact),
                    typeof (Post),
                    typeof (Tag),

                };
            Children = new List<Type>();
        }

        protected virtual DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            if (!Roots.Contains(typeof(TEntity)))
                throw new BusinessServicesException(String.Format("'{0}' is not a root entity.",typeof(TEntity)));
            return Context.Set<TEntity>();
        }

        protected virtual void SetEntityState(object entity, EntityState entityState)
        {
            Context.Entry(entity).State = entityState;
        }

        public void AddChild<TEntity>(object o) where TEntity : class
        {
            if (Children == null || !Children.Contains(typeof(TEntity)))
            {
                throw new BusinessServicesException(String.Format("'{0}' is not a child entity.", typeof(TEntity)));
            }
            var repository = new Repository<TEntity>(Context, UnitOfWork);
            repository.Add((TEntity)o);
        }

        public void DeleteChild<TEntity>(int id) where TEntity : class, IDeletableEntity
        {
            if (Children == null || !Children.Contains(typeof(TEntity)))
            {
                throw new BusinessServicesException(String.Format("'{0}' is not a child entity.", typeof(TEntity)));
            }
            var repository = new Repository<TEntity>(Context, UnitOfWork);
            repository.Delete(id);
        }

        public void ArchiveChild<TEntity>(object o) where TEntity : class, IArchivableEntity, IEntityBase
        {
            if (Children == null || !Children.Contains(typeof(TEntity)))
            {
                throw new BusinessServicesException(String.Format("'{0}' is not a child entity.", typeof(TEntity)));
            }
            var repository = new Repository<TEntity>(Context, UnitOfWork);
            repository.Archive((TEntity)o);
        }

        public void SetActiveChild<TEntity>(object o) where TEntity : class, IEntityBase
        {
            if (Children == null || !Children.Contains(typeof(TEntity)))
            {
                throw new BusinessServicesException(String.Format("'{0}' is not a child entity.", typeof(TEntity)));
            }
            var repository = new Repository<TEntity>(Context, UnitOfWork);
            repository.SetActive((TEntity)o);
        }

        public void SetInActiveChild<TEntity>(object o) where TEntity : class, IEntityBase
        {
            if (Children == null || !Children.Contains(typeof(TEntity)))
            {
                throw new BusinessServicesException(String.Format("'{0}' is not a child entity.", typeof(TEntity)));
            }
            var repository = new Repository<TEntity>(Context, UnitOfWork);
            repository.SetInActive((TEntity)o);
        }

        public void UpdateChild<TEntity>(object o) where TEntity : class
        {
            if (Children == null || !Children.Contains(typeof(TEntity)))
            {
                throw new BusinessServicesException(String.Format("'{0}' is not a child entity.", typeof(TEntity)));
            }
            var repository = new Repository<TEntity>(Context, UnitOfWork);
            repository.Update((TEntity)o);
        }

        public IEnumerable<TEntity> GetAllChild<TEntity>() where TEntity : class
        {
            if (Children == null || !Children.Contains(typeof(TEntity)))
            {
                throw new BusinessServicesException(String.Format("'{0}' is not a child entity.", typeof(TEntity)));
            }
            var repository = new Repository<TEntity>(Context, UnitOfWork);
            return repository.GetAll();
        }

        public IEnumerable<TEntity> FindChild<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class
        {
            if (Children == null || !Children.Contains(typeof (TEntity)))
            {
                throw new BusinessServicesException(String.Format("'{0}' is not a child entity.", typeof (TEntity)));
            }
            var repository = new Repository<TEntity>(Context, UnitOfWork);
            return repository.Find(@where);
        }
    }
}
