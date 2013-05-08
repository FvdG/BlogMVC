using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Blog.Domain.Interfaces;
using Blog.Domain.Models;

namespace Blog.Data.Repositories
{
    public class TagRepository : BaseRepository, ITagRepository
    {
        public TagRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork) { }

        public IQueryable<Tag> Tags()
        {
            return GetDbSet<Tag>().Include(tag => tag.Posts);
        }

        public List<Tag> TagsOfPost(int postId)
        {
            return GetDbSet<Tag>().Where(i => i.Posts.Any(a => a.PostId == postId)).Distinct().ToList();
        }

        public int TotalTags()
        {
            return GetDbSet<Tag>().Count();
        }

        public Tag Tag(string slug)
        {
            return GetDbSet<Tag>().FirstOrDefault(t => t.UrlSlug.Equals(slug));
        }

        public Tag Tag(int id)
        {
            return GetDbSet<Tag>().Include(tag => tag.Posts).SingleOrDefault(t => t.TagId == id);
        }

        public int AddTag(Tag tag)
        {
            GetDbSet<Tag>().Add(tag);
            UnitOfWork.SaveChanges();
            return tag.TagId;
        }

        public void EditTag(Tag tag)
        {
            var oldTag = GetDbSet<Tag>().Include(t => t.Posts).SingleOrDefault(t => t.TagId == tag.TagId);
            if (oldTag == null) return;
            oldTag.Name = tag.Name;
            oldTag.Description = tag.Description;
            oldTag.UrlSlug = tag.UrlSlug;
            oldTag.Posts = tag.Posts;

            SetEntityState(oldTag, EntityState.Modified);
            UnitOfWork.SaveChanges();
        }

        public void DeleteTag(int id)
        {
            var tag = GetDbSet<Tag>().SingleOrDefault(t => t.TagId == id);
            if (tag == null) return;
            GetDbSet<Tag>().Remove(tag);
            UnitOfWork.SaveChanges();
        }
    }
}
