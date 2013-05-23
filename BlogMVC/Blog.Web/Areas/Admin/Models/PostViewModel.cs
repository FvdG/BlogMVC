using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Blog.Domain.Models;

namespace Blog.Web.Areas.Admin.Models
{
    /// <summay>
    /// This class is used for JSON serialization and deserialization. It is mapped to and fom the Post entity class
    /// </summay>
    public class PostViewModel
    {
        [Key]
        public int PostId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string Description { get; set; }
        public string Meta { get; set; }
        [Required]
        public string UrlSlug { get; set; }
        public bool Published { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime Modified { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}