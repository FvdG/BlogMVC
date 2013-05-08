using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Blog.Domain.Interfaces;
using Blog.Domain.Models;

namespace Blog.Data.Repositories
{
    public class PostRepository : BaseRepository, IPostRepository
    {
        public PostRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<Post> Posts(int pageNo, int pageSize)
        {
            IQueryable<Post> posts = GetDbSet<Post>()
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .Where(p => p.Published)
                .OrderByDescending(p => p.PostedOn)
                .Skip(pageNo*pageSize)
                .Take(pageSize);

            return posts.ToList();
        }

        public List<Post> PostsForTag(string tagSlug, int pageNo, int pageSize)
        {
            IQueryable<Post> posts = GetDbSet<Post>()
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                .OrderByDescending(p => p.PostedOn)
                .Skip(pageNo*pageSize)
                .Take(pageSize);
            return posts.ToList();
        }

        public List<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize)
        {
            IQueryable<Post> posts = GetDbSet<Post>()
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                .OrderByDescending(p => p.PostedOn)
                .Skip(pageNo*pageSize)
                .Take(pageSize);

            return posts.ToList();
        }

        public List<Post> PostsForSearch(string search, int pageNo, int pageSize)
        {
            IQueryable<Post> posts = GetDbSet<Post>()
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .Where(
                    p =>
                    p.Published &&
                    (p.Title.Contains(search) || p.Category.Name.Equals(search) ||
                     p.Tags.Any(t => t.Name.Equals(search))))
                .OrderByDescending(p => p.PostedOn)
                .Skip(pageNo*pageSize)
                .Take(pageSize);

            return posts.ToList();
        }

        public int TotalPosts(bool checkIsPublished = true)
        {
            return GetDbSet<Post>().Count();
        }

        public int TotalPostsForCategory(string categorySlug)
        {
            return GetDbSet<Post>().Include(p => p.Category)
                                   .Count(p => p.Published && p.Category.UrlSlug.Equals(categorySlug));
        }

        public int TotalPostsForTag(string tagSlug)
        {
            return GetDbSet<Post>().Include(p => p.Tags)
                                   .Count(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)));
        }

        public int TotalPostsForSearch(string search)
        {
            return GetDbSet<Post>()
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .Count(
                    p =>
                    p.Published &&
                    (p.Title.Contains(search) || p.Category.Name.Equals(search) ||
                     p.Tags.Any(t => t.Name.Equals(search))));
        }

        public List<Post> Posts(int pageNo, int pageSize, string sortColumn, bool sortByAscending)
        {
            IQueryable<Post> query;

            switch (sortColumn)
            {
                case "Title":
                    if (sortByAscending)
                        query = GetDbSet<Post>()
                            .Include(p => p.Category)
                            .OrderBy(p => p.Title)
                            .Skip(pageNo*pageSize)
                            .Take(pageSize);
                    else
                        query = GetDbSet<Post>()
                            .Include(p => p.Category)
                            .OrderByDescending(p => p.Title)
                            .Skip(pageNo*pageSize)
                            .Take(pageSize);
                    break;
                case "Published":
                    if (sortByAscending)
                        query = GetDbSet<Post>()
                            .Include(p => p.Category)
                            .OrderBy(p => p.Published)
                            .Skip(pageNo*pageSize)
                            .Take(pageSize);
                    else
                        query = GetDbSet<Post>()
                            .Include(p => p.Category)
                            .OrderByDescending(p => p.Published)
                            .Skip(pageNo*pageSize)
                            .Take(pageSize);
                    break;
                case "PostedOn":
                    if (sortByAscending)
                        query = GetDbSet<Post>()
                            .Include(p => p.Category)
                            .OrderBy(p => p.PostedOn)
                            .Skip(pageNo*pageSize)
                            .Take(pageSize);
                    else
                        query = GetDbSet<Post>()
                            .Include(p => p.Category)
                            .OrderByDescending(p => p.PostedOn)
                            .Skip(pageNo*pageSize)
                            .Take(pageSize);
                    break;
                case "Modified":
                    if (sortByAscending)
                        query = GetDbSet<Post>()
                            .Include(p => p.Category)
                            .OrderBy(p => p.Modified)
                            .Skip(pageNo*pageSize)
                            .Take(pageSize);
                    else
                        query = GetDbSet<Post>()
                            .Include(p => p.Category)
                            .OrderByDescending(p => p.Modified)
                            .Skip(pageNo*pageSize)
                            .Take(pageSize);
                    break;
                case "Category":
                    if (sortByAscending)
                        query = GetDbSet<Post>()
                            .Include(p => p.Category)
                            .OrderBy(p => p.Category.Name)
                            .Skip(pageNo*pageSize)
                            .Take(pageSize);
                    else
                        query = GetDbSet<Post>()
                            .Include(p => p.Category)
                            .OrderByDescending(p => p.Category.Name)
                            .Skip(pageNo*pageSize)
                            .Take(pageSize);
                    break;
                default:
                    query = GetDbSet<Post>()
                        .Include(p => p.Category)
                        .OrderByDescending(p => p.PostedOn)
                        .Skip(pageNo*pageSize)
                        .Take(pageSize);
                    break;
            }
            query.Include(p => p.Tags);
            return query.ToList();
        }

        public IQueryable<Post> Posts()
        {
           return GetDbSet<Post>()
                .Include(p => p.Category)
                //.Include(p => p.Tags)
                .OrderBy(p => p.Category.Name);
        }

        public Post Post(int year, int month, string titleSlug)
        {
            return GetDbSet<Post>()
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .FirstOrDefault(p => p.PostedOn.Year == year && p.PostedOn.Month == month && p.UrlSlug.Equals(titleSlug));

        }

        public Post Post(int id)
        {
            return GetDbSet<Post>()
                .Include(p => p.Category)
                .Include(p => p.Category.Posts)
                .Include(p => p.Tags.Select(t => t.Posts))
                .SingleOrDefault(p => p.PostId == id);
        }

        public int AddPost(Post post)
        {
            GetDbSet<Post>().Add(post);
            UnitOfWork.SaveChanges();
            return post.PostId;
        }

        public void EditPost(Post post)
        {
            Post oldPost = GetDbSet<Post>()
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .SingleOrDefault(p => p.PostId == post.PostId);
            if (oldPost == null) return;
            oldPost.Description = post.Description;
            oldPost.Category = post.Category;
            oldPost.Meta = post.Meta;
            oldPost.Modified = post.Modified;
            oldPost.PostedOn = post.PostedOn;
            oldPost.Published = post.Published;
            oldPost.ShortDescription = post.ShortDescription;
            oldPost.Tags = post.Tags;
            oldPost.Title = post.Title;
            oldPost.UrlSlug = post.UrlSlug;

            SetEntityState(oldPost, EntityState.Modified);
            UnitOfWork.SaveChanges();
        }

        public void DeletePost(int id)
        {
            Post post = GetDbSet<Post>().SingleOrDefault(p => p.PostId == id);
            if (post == null) return;
            GetDbSet<Post>().Remove(post);
            UnitOfWork.SaveChanges();
        }
    }
}