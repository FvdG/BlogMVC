using System.Collections.Generic;
using Blog.Domain.Models;

namespace Blog.Domain.Interfaces
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetRoles();
        Role GetRole(int roleId);
    }
}
