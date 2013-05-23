using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Areas.Admin.Models
{
    public class TagViewModel
    {
        [Key]
        public int TagId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string UrlSlug { get; set; }
        [Required]
        public string Description { get; set; }
    }
}