using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Areas.Admin.Models
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string UrlSlug { get; set; }
        [Required]
        public string Description { get; set; }
    }
}