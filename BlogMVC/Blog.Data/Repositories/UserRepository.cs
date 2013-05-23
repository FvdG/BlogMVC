using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Helpers;
using Blog.Domain.Interfaces;
using Blog.Domain.Models;

namespace Blog.Data.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IQueryable<User> GetUsers()
        {
            return GetDbSet<User>();
        }

        public User GetUser(int userId)
        {
            return GetDbSet<User>().SingleOrDefault(u => u.UserId == userId);
        }

        public void CreateUser(string username, string password, Role role)
        {
            var newUser = new User
                {
                    UserName = username,
                    Roles = new Collection<Role> {role}
                };
            GetDbSet<User>().Add(newUser);
            UnitOfWork.SaveChanges();
            GetDbSet<Membership>().Add(new Membership
            {
                UserId = 1,
                CreateDate = DateTime.Now,
                IsConfirmed = true,
                Password = Crypto.HashPassword(password),
                PasswordChangedDate = DateTime.Now,
                PasswordSalt = ""
            });
            UnitOfWork.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            var oldUser = GetDbSet<User>().Include(u => u.Roles).SingleOrDefault(u => u.UserId == user.UserId);
            if (oldUser == null) return;
            oldUser.UserName = user.UserName;
            oldUser.Roles = user.Roles;
            SetEntityState(oldUser, EntityState.Modified);
            UnitOfWork.SaveChanges();
        }

        public void ChangeRole(int userId, Role newRole)
        {
            //I Decided 1 role per user is needed.
            var user = GetDbSet<User>().SingleOrDefault(u => u.UserId == userId);
            if (user == null) return;
            user.Roles.Clear();
            user.Roles.Add(newRole);
            UnitOfWork.SaveChanges();
        }

        public void RemoveUser(int userId)
        {
            var user = GetDbSet<User>().SingleOrDefault(u => u.UserId == userId);
            var membership = GetDbSet<Membership>().SingleOrDefault(m => m.UserId == userId);
            if (user == null || membership == null) return;
            GetDbSet<User>().Remove(user);
            GetDbSet<Membership>().Remove(membership);
            UnitOfWork.SaveChanges();
        }
    }
}
