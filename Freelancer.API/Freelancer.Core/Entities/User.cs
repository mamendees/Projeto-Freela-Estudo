
namespace Freelancer.Core.Entities;
public class User : BaseEntity
{
    public User(string fullName, string email, DateTime birthDate, string password, string role)
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
        CreatedAt = DateTime.Now;
        Active = true;
        Password = password;
        Role = role;

        Skills = new List<UserSkill>();
        OwnedProjects = new List<Project>();
        FreelanceProjects = new List<Project>();
    }

    public string FullName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public DateTime BirthDate { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool Active { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }

    public List<UserSkill> Skills { get; private set; }
    public List<Project> OwnedProjects { get; private set; }
    public List<Project> FreelanceProjects { get; private set; }
    public List<ProjectComment> Comments { get; set; }
}
