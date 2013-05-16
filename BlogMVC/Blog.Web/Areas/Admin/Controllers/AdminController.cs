using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Blog.Domain.Interfaces;
using Blog.Domain.Models;
using Blog.Web.Areas.Admin.Models;
using Blog.Web.Filters;
using Blog.Web.Models;
using Newtonsoft.Json;
using WebMatrix.WebData;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AdminController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;

        public AdminController(IPostRepository postRepository, ICategoryRepository categoryRepository,
            ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
        }

        #region Categories

        public ActionResult CategoryGrid()
        {
            return PartialView("_CategoryGrid");
        }

        /// <summary>
        /// Reads the available categories to provide data for the Kendo Grid
        /// </summary>
        /// <returns>All available categories as JSON</returns>
        [HttpPost]
        public ActionResult ReadCategories(int take, int skip, IEnumerable<Sort> sort, Blog.Web.Models.Filter filter)
        {
            var categories = Mapper.Map<Category[], CategoryViewModel[]>(_categoryRepository.Categories().ToArray());
            DataSourceResult result = categories.AsQueryable().ToDataSourceResult(take, skip, sort, filter);
            return Json(result);
        }

        /// <summary>
        /// Creates new category by inserting the data posted by the Kendo Grid in the database.
        /// </summary>
        /// <param name="categories">The Categories created by the user.</param>
        /// <returns>The inserted categories so the Kendo Grid is aware of the database generated PostId</returns>
        [HttpPost]
        public ActionResult CreateCategory(IEnumerable<CategoryViewModel> categories)
        {
            var categoryViewModels = categories as CategoryViewModel[] ?? categories.ToArray();
            if (categoryViewModels.Any())
            {
                //Get first because at the moment no multiple update available
                CategoryViewModel categoryViewModel = categoryViewModels.FirstOrDefault();
                if (categoryViewModel != null)
                {
                    var category = new Category();
                    {
                        category.Name = categoryViewModel.Name;
                        category.Description = categoryViewModel.Description;
                        category.UrlSlug = categoryViewModel.UrlSlug;
                    }
                    _categoryRepository.AddCategory(category);
                    return Json(categoryViewModel);
                }
            }
            return null;
        }

        /// <summary>
        /// Updates existing categories by updating the database with the data posted by the Kendo Grid.
        /// </summary>
        /// <param name="categories">The categories updated by the user</param>
        [HttpPost]
        public JsonResult UpdateCategory(IEnumerable<CategoryViewModel> categories)
        {
            var categoryViewModels = categories as CategoryViewModel[] ?? categories.ToArray();
            if (categoryViewModels.Any())
            {
                //Get first because at the moment no multiple update available
                CategoryViewModel categoryViewModel = categoryViewModels.FirstOrDefault();
                if (categoryViewModel != null)
                {
                    Category category = _categoryRepository.Category(categoryViewModel.CategoryId);
                    if (category != null)
                    {
                        category.Name = categoryViewModel.Name;
                        category.Description = categoryViewModel.Description;
                        category.UrlSlug = categoryViewModel.UrlSlug;
                    }
                    _categoryRepository.EditCategory(category);
                    return Json(categoryViewModel);
                }
            }
            return null;
        }

        /// <summary>
        /// Destroys existing categories by deleting them from the database.
        /// </summary>
        /// <param name="categories">The category deleted by the user</param>
        [HttpPost]
        public JsonResult DeleteCategory(IEnumerable<CategoryViewModel> categories)
        {
            var categoryViewModels = categories as CategoryViewModel[] ?? categories.ToArray();
            if (categoryViewModels.Any())
            {
                //Get first because at the moment no multiple delete available
                CategoryViewModel categoryViewModel = categoryViewModels.FirstOrDefault();
                if (categoryViewModel != null)
                {
                    _categoryRepository.DeleteCategory(categoryViewModel.CategoryId);
                    //Return emtpy result
                    return Json(string.Empty);
                }
            }
            return null;
        }

        #endregion

        #region Tags

        public ActionResult TagGrid()
        {
            return PartialView("_TagGrid");
        }

        /// <summary>
        /// Reads the available tags to provide data for the Kendo Grid
        /// </summary>
        /// <returns>All available tags as JSON</returns>
        [HttpPost]
        public ActionResult ReadTags(int take, int skip, IEnumerable<Sort> sort, Blog.Web.Models.Filter filter)
        {
            var tags = Mapper.Map<Tag[], TagViewModel[]>(_tagRepository.Tags().ToArray());
            DataSourceResult result = tags.AsQueryable().ToDataSourceResult(take, skip, sort, filter);
            return Json(result);
        }

        /// <summary>
        /// Creates new tags by inserting the data posted by the Kendo Grid in the database.
        /// </summary>
        /// <param name="tags">The tags created by the user.</param>
        /// <returns>The inserted tags so the Kendo Grid is aware of the database generated PostId</returns>
        [HttpPost]
        public ActionResult CreateTag(IEnumerable<TagViewModel> tags)
        {
            var tagsViewModels = tags as TagViewModel[] ?? tags.ToArray();
            if (tagsViewModels.Any())
            {
                //Get first because at the moment no multiple update available
                var tagViewModel = tagsViewModels.FirstOrDefault();
                if (tagViewModel != null)
                {
                    var tag = new Tag();
                    {
                        tag.Name = tagViewModel.Name;
                        tag.Description = tagViewModel.Description;
                        tag.UrlSlug = tagViewModel.UrlSlug;
                    }
                    _tagRepository.AddTag(tag);
                    return Json(tagViewModel);
                }
            }
            return null;
        }

        /// <summary>
        /// Updates existing tags by updating the database with the data posted by the Kendo Grid.
        /// </summary>
        /// <param name="tags">The tags updated by the user</param>
        [HttpPost]
        public JsonResult UpdateTag(IEnumerable<TagViewModel> tags)
        {
            var tagViewModels = tags as TagViewModel[] ?? tags.ToArray();
            if (tagViewModels.Any())
            {
                //Get first because at the moment no multiple update available
                var tagViewModel = tagViewModels.FirstOrDefault();
                if (tagViewModel != null)
                {
                    var tag = _tagRepository.Tag(tagViewModel.TagId);
                    if (tag != null)
                    {
                        tag.Name = tagViewModel.Name;
                        tag.Description = tagViewModel.Description;
                        tag.UrlSlug = tagViewModel.UrlSlug;
                    }
                    _tagRepository.EditTag(tag);
                    return Json(tagViewModel);
                }
            }
            return null;
        }

        /// <summary>
        /// Destroys existing tags by deleting them from the database.
        /// </summary>
        /// <param name="tags">The tag deleted by the user</param>
        [HttpPost]
        public JsonResult DeleteTag(IEnumerable<TagViewModel> tags)
        {
            var tagViewModels = tags as TagViewModel[] ?? tags.ToArray();
            if (tagViewModels.Any())
            {
                //Get first because at the moment no multiple delete available
                var tagViewModel = tagViewModels.FirstOrDefault();
                if (tagViewModel != null)
                {
                    _tagRepository.DeleteTag(tagViewModel.TagId);
                    //Return emtpy result
                    return Json(string.Empty);
                }
            }
            return null;
        }

        #endregion

        #region Posts

        public ActionResult GetSelectedTags(int postId)
        {
            var data = _postRepository.Post(postId).Tags.Select(t => t.TagId).ToList();
            return Content(JsonConvert.SerializeObject(data, Formatting.Indented), "application/json");
        }


        public ActionResult GetAllTags()
        {
            var data =  _tagRepository.Tags().ToList();
            return Content(JsonConvert.SerializeObject(data, Formatting.Indented), "application/json");
        }

        public ActionResult PostGrid()
        {
            //ViewData["Category_Data"] = new SelectList(_categoryRepository.Categories(), "CategoryId", "Name");
            return PartialView("_PostGrid");
        }

        /// <summary>
        /// Creates new posts by inserting the data posted by the Kendo Grid in the database.
        /// </summary>
        /// <param name="postViewModel">The Post created by the user.</param>
        /// <returns>The inserted products so the Kendo Grid is aware of the database generated PostId</returns>
        [HttpPost]
        public ActionResult CreatePost(PostViewModel postViewModel)
        {
            return null;
        }

        /// <summary>
        /// Reads the available posts to provide data for the Kendo Grid
        /// </summary>
        /// <returns>All available posts as JSON</returns>
        [HttpPost]
        public ActionResult ReadPosts(int take, int skip, IEnumerable<Sort> sort, Blog.Web.Models.Filter filter)
        {
            var posts = Mapper.Map<Post[], PostViewModel[]>(_postRepository.Posts().ToArray());
            DataSourceResult result = posts.AsQueryable().ToDataSourceResult(take, skip, sort, filter);
            return Json(result);
        }

        /// <summary>
        /// Create post in the database with the data posted by the Kendo Grid.
        /// </summary>
        /// <param name="posts">The post created by the user</param>
        [HttpPost]
        public JsonResult CreatePost(IEnumerable<PostViewModel> posts)
        {
            var postViewModels = posts as PostViewModel[] ?? posts.ToArray();
            if (postViewModels.Any())
            {
                //Get first because at the moment no multiple create available
                PostViewModel postViewModel = postViewModels.FirstOrDefault();
                if (postViewModel != null)
                {
                    var post = new Post();
                    {
                        post.Title = postViewModel.Title;
                        post.ShortDescription = postViewModel.ShortDescription;
                        post.Description = postViewModel.Description;
                        post.Meta = postViewModel.Meta;
                        post.UrlSlug = postViewModel.UrlSlug;
                        post.Published = postViewModel.Published;
                        post.Category = _categoryRepository.Category(postViewModel.CategoryId);
                        post.PostedOn = DateTime.Now;
                    }        
                    _postRepository.AddPost(post);
                    return Json(postViewModel);
                }        
            }
            return null;
        }

        /// <summary>
        /// Updates existing posts by updating the database with the data posted by the Kendo Grid.
        /// </summary>
        /// <param name="posts">The post updated by the user</param>
        [HttpPost]
        public JsonResult UpdatePost(IEnumerable<PostViewModel> posts)
        {
            var postViewModels = posts as PostViewModel[] ?? posts.ToArray();
            if (postViewModels.Any())
            {
                //Get first because at the moment no multiple update available
                PostViewModel postViewModel = postViewModels.FirstOrDefault();
                if (postViewModel != null)
                {
                    Post post = _postRepository.Post(postViewModel.PostId);
                    if (post != null)
                    {
                        post.Title = postViewModel.Title;
                        post.ShortDescription = postViewModel.ShortDescription;
                        post.Description = postViewModel.Description;
                        post.Meta = postViewModel.Meta;
                        post.UrlSlug = postViewModel.UrlSlug;
                        post.Published = postViewModel.Published;
                        post.Category = _categoryRepository.Category(postViewModel.CategoryId);
                        post.Modified = DateTime.Now;
                        post.Tags.Clear();
                        if (postViewModel.Tags != null)
                        {
                            foreach (var tag in postViewModel.Tags)
                            {
                                post.Tags.Add(_tagRepository.Tag(tag.TagId));
                            }
                        }
                    }
                    _postRepository.EditPost(post);
                    return Json(postViewModel);
                }        
            }
            return null;
        }

        /// <summary>
        /// Destroys existing posts by deleting them from the database.
        /// </summary>
        /// <param name="posts">The post deleted by the user</param>
        [HttpPost]
        public JsonResult DeletePost(IEnumerable<PostViewModel> posts)
        {
            var postViewModels = posts as PostViewModel[] ?? posts.ToArray();
            if (postViewModels.Any())
            {
                //Get first because at the moment no multiple delete available
                PostViewModel postViewModel = postViewModels.FirstOrDefault();
                if (postViewModel != null)
                {
                    _postRepository.DeletePost(postViewModel.PostId);
                    //Return emtpy result
                    return Json(string.Empty);
                }
            }
            return null;
        }

        public ActionResult GetAllCategories()
        {
            var data = _categoryRepository.Categories();
            return Content(JsonConvert.SerializeObject(data, Formatting.Indented), "application/json");
        }

        public ActionResult GetTagsForPost(int id)
        {
            var data = _tagRepository.TagsOfPost(id);
            return Content(JsonConvert.SerializeObject(data, Formatting.Indented), "application/json");
        }

        #endregion

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToUrl(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            WebSecurity.Logout();

            return RedirectToAction("Login", "Admin", new { area = "Admin"});
        }

        private ActionResult RedirectToUrl(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Admin", new { area = "Admin" });
            
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
