using System.Collections.Generic;
using Blog.Domain.Interfaces;
using Newtonsoft.Json;

namespace Blog.Domain.Models
{
    public class Category : IDeletableEntity
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public List<Post> Posts { get; set; }
    }
}
