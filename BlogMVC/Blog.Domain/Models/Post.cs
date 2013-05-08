using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Blog.Domain.Interfaces;
using Newtonsoft.Json;

namespace Blog.Domain.Models
{
    public class Post : IDeletableEntity
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string ShortDescription { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Meta { get; set; }
        public string UrlSlug { get; set; }
        public bool Published { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime? Modified { get; set; }

        public Category Category { get; set; }

        [JsonIgnore]
        public List<Tag> Tags { get; set; }
    }
}
