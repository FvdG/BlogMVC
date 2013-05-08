using System.Collections.Generic;
using Blog.Domain.Interfaces;
using Newtonsoft.Json;

namespace Blog.Domain.Models
{
    public class Tag : IDeletableEntity
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public virtual List<Post> Posts { get; set; }
    }
}
