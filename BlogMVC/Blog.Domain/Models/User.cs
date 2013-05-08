using System.Collections.Generic;

namespace Blog.Domain.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
