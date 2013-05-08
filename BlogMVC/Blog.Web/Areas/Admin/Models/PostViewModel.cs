using System;
using System.Collections.Generic;
using Blog.Domain.Models;

namespace Blog.Web.Areas.Admin.Models
{
    /// <summay>
    /// This class is used for JSON serialization and deserialization. It is mapped to and fom the Post entity class
    /// </summay>
    public class PostViewModel
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Meta { get; set; }
        public string UrlSlug { get; set; }
        public bool Published { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime Modified { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public IList<Tag> Tags { get; set; }
    }
}