using System.Collections.Generic;
using Blog.Domain.Interfaces;
using Blog.Domain.Models;
using System.Linq;

namespace Blog.Data.Repositories
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public RoleRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork) { }

        public IEnumerable<Role> GetRoles()
        {
            return GetDbSet<Role>();
        }

        public Role GetRole(int roleId)
        {
            return GetDbSet<Role>().SingleOrDefault(r => r.RoleId == roleId);
        }
    }
}
