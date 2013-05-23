using AutoMapper;
using Blog.Domain.Models;
using Blog.Web.Areas.Admin.Models;
using System.Linq;

namespace Blog.Web.App_Start
{
    public static class MapperConfig
    {
        public static void RegisterMap()
        {
            CreatePostAutoMapping();
            CreateCategoryAutoMapping();
            CreateTagAutoMapping();
            CreateUserAutoMapping();
        }

        private static void CreatePostAutoMapping()
        {
            Mapper.CreateMap<Post, PostViewModel>()
                .ForMember(pvm => pvm.CategoryName, a => a.MapFrom(o => o.Category.Name))
                .ForMember(pvm => pvm.CategoryId, a => a.MapFrom(o => o.Category.CategoryId));      
        }

        private static void CreateCategoryAutoMapping()
        {
            Mapper.CreateMap<Category, CategoryViewModel>();
        }

        private static void CreateTagAutoMapping()
        {
            Mapper.CreateMap<Tag, TagViewModel>();
        }

        private static void CreateUserAutoMapping()
        {
            Mapper.CreateMap<User, UserViewModel>()
                  .ForMember(uvm => uvm.UserId, a => a.MapFrom(o => o.UserId))
                  .ForMember(uvm => uvm.Username, a => a.MapFrom(o => o.UserName))
                  .ForMember(uvm => uvm.RoleId, a => a.MapFrom(o => o.Roles.FirstOrDefault().RoleId));
        }
    }
}