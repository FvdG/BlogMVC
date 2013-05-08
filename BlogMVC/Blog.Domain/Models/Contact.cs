using Blog.Domain.Interfaces;

namespace Blog.Domain.Models
{
    public class Contact : IDeletableEntity
    {
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public string Website { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
