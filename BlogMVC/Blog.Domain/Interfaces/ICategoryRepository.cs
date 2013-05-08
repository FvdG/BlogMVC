using System.Collections.Generic;
using System.Linq;
using Blog.Domain.Models;

namespace Blog.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories();
        int TotalCategories();
        Category Category(string categorySlug);
        Category Category(int id);
        int AddCategory(Category category);
        void EditCategory(Category category);
        void DeleteCategory(int id);
    }
}
