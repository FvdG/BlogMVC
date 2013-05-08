using System.Collections.Generic;

namespace Blog.Domain.Models
{
    //Pre-defined roles from the system
    public enum Roles
    {
        Administrator = 1,
        SuperUser = 2,
        Editor = 3,
        User = 4
    }

    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
