using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using Blog.Domain.Interfaces;
using Blog.Domain.Models;

namespace Blog.Data.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork) { }

        public IQueryable<Category> Categories()
        {
            return GetDbSet<Category>().Include(category => category.Posts).OrderBy(c => c.Name);
        }

        public int TotalCategories()
        {
            return GetDbSet<Category>().Count();
        }

        public Category Category(string slug)
        {
            return GetDbSet<Category>().FirstOrDefault(t => t.UrlSlug.Equals(slug));
        }

        public Category Category(int id)
        {
            return GetDbSet<Category>().SingleOrDefault(t => t.CategoryId == id);
        }

        public int AddCategory(Category category)
        {
            GetDbSet<Category>().Add(category);
            UnitOfWork.SaveChanges();
            return category.CategoryId;
        }

        public void EditCategory(Category category)
        {
            var oldCategory = GetDbSet<Category>().SingleOrDefault(c => c.CategoryId == category.CategoryId);
            if (oldCategory == null) return;
            oldCategory.Name = category.Name;
            oldCategory.Description = category.Description;
            oldCategory.UrlSlug = category.UrlSlug;
            oldCategory.Posts = category.Posts;

            SetEntityState(oldCategory, EntityState.Modified);
            UnitOfWork.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = GetDbSet<Category>().SingleOrDefault(c => c.CategoryId == id);
            if (category == null) return;
            GetDbSet<Category>().Remove(category);
            UnitOfWork.SaveChanges();
        }
    }
}
