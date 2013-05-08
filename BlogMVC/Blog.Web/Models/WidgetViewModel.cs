using System.Linq;
using Blog.Domain.Interfaces;
using Blog.Domain.Models;
using System.Collections.Generic;

namespace Blog.Web.Models
{
  public class WidgetViewModel
  {
    public WidgetViewModel(ICategoryRepository categoryRepository, ITagRepository tagRepository,
        IPostRepository postRepository)
    {
        Categories = categoryRepository.Categories().ToList();
        Tags = tagRepository.Tags().ToList();
        LatestPosts = postRepository.Posts(0, 10);
    }

    public IList<Category> Categories { get; private set; }
    public IList<Tag> Tags { get; private set; }
    public IList<Post> LatestPosts { get; private set; }
  }
}