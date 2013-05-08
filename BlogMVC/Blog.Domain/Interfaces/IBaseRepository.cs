using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Blog.Domain.Models;

namespace Blog.Domain.Interfaces
{
    public interface IBaseRepository
    {
        void AddChild<TEntity>(object o) where TEntity : class;
        IEnumerable<TEntity> GetAllChild<TEntity>() where TEntity : class;
        IEnumerable<TEntity> FindChild<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class;
        void DeleteChild<TEntity>(int id) where TEntity : class, IDeletableEntity;
        void UpdateChild<TEntity>(object o) where TEntity : class;
        void ArchiveChild<TEntity>(object o) where TEntity : class, IArchivableEntity, IEntityBase;
        void SetActiveChild<TEntity>(object o) where TEntity : class, IEntityBase;
        void SetInActiveChild<TEntity>(object o) where TEntity : class, IEntityBase;
    }
}
