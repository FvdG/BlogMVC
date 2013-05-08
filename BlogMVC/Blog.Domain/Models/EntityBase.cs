namespace Blog.Domain.Models
{
    public enum State
    {
        Active = 1,
        InActive = 2,
        Archived = 3
    }

    public interface IEntityBase
    {
        State State { get; set; }
    }

    public abstract class EntityBase : IEntityBase
    {
        public State State { get; set; }
    }
}
