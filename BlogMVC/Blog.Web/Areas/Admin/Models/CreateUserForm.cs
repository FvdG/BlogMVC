namespace Blog.Web.Areas.Admin.Models
{
    public class CreateUserForm
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}