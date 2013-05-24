using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Domain.Interfaces;
using Blog.Domain.Models;

namespace Blog.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly IUserRepository _userRepository;

        public BaseController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private User _currentUser;
        public User CurrentUser
        {
            get
            {
                return _currentUser ??
                       (_currentUser = _userRepository.GetByUsername(User.Identity.Name));
            }
        }
    }
}
