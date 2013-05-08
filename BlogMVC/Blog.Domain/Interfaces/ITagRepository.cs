using System.Collections.Generic;
using System.Linq;
using Blog.Domain.Models;

namespace Blog.Domain.Interfaces
{
    public interface ITagRepository
    {
        IQueryable<Tag> Tags();
        List<Tag> TagsOfPost(int postId);
        int TotalTags();
        Tag Tag(string tagSlug);
        Tag Tag(int id);
        int AddTag(Tag tag);
        void EditTag(Tag tag);
        void DeleteTag(int id);
    }
}
