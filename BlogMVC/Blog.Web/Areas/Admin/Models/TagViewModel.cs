namespace Blog.Web.Areas.Admin.Models
{
    public class TagViewModel
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Description { get; set; }
    }
}