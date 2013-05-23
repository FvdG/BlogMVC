using System.Linq;
using Blog.Domain.Models;

namespace Blog.Domain.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> GetUsers();
        User GetUser(int userId);
        void CreateUser(string username, string password, Role role);
        void UpdateUser(User user);
        void ChangeRole(int userId, Role newRole);
        void RemoveUser(int userId);
    }
}
