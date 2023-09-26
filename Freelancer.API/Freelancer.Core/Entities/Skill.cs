namespace Freelancer.Core.Entities;
public class Skill : BaseEntity
{
    public Skill()
    {
    }

    public Skill(string description)
    {
        Description = description;
    }

    public Skill(string description, DateTime createdAt)
    {
        Description = description;
        CreatedAt = createdAt;
    }

    public string Description { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
}
