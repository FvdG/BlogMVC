using System.Collections.Generic;
using System.Linq;
using Blog.Domain.Models;

namespace Blog.Domain.Interfaces
{
    public interface IPostRepository
    {
        List<Post> Posts(int pageNo, int pageSize);
        List<Post> PostsForTag(string tagSlug, int pageNo, int pageSize);
        List<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize);
        List<Post> PostsForSearch(string search, int pageNo, int pageSize);

        int TotalPosts(bool checkIsPublished = true);
        int TotalPostsForCategory(string categorySlug);
        int TotalPostsForTag(string tagSlug);
        int TotalPostsForSearch(string search);

        List<Post> Posts(int pageNo, int pageSize, string sortColumn, bool sortByAscending);
        IQueryable<Post> Posts();
        Post Post(int year, int month, string titleSlug);
        Post Post(int id);
        int AddPost(Post post);
        void EditPost(Post post);
        void DeletePost(int id);
    }
}
