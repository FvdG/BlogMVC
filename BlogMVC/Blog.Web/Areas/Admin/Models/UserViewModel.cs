using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Areas.Admin.Models
{
    public class UserViewModel
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [UIHint("GridForeignKey")]
        public int RoleId { get; set; }
    }
}