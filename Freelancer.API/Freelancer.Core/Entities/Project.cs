using Freelancer.Core.Enums;

namespace Freelancer.Core.Entities;
public class Project : BaseEntity
{
    public Project(string title, string description, int idClient, int idFreelancer, decimal totalCost)
    {
        Title = title;
        Description = description;
        IdClient = idClient;
        IdFreelancer = idFreelancer;
        TotalCost = totalCost;

        CreatedAt = DateTime.Now;
        Status = ProjectStatusEnum.Created;
        Comments = new List<ProjectComment>();
    }

    public Project(string title, string description, int idClient, int idFreelancer, decimal totalCost, User client, User freelancer)
    {
        Title = title;
        Description = description;
        IdClient = idClient;
        IdFreelancer = idFreelancer;
        TotalCost = totalCost;

        CreatedAt = DateTime.Now;
        Status = ProjectStatusEnum.Created;
        Comments = new List<ProjectComment>();
        Client = client;
        Freelancer = freelancer;
    }

    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public int IdClient { get; private set; }
    public User Client { get; private set; }
    public int IdFreelancer { get; private set; }
    public User Freelancer { get; private set; }
    public decimal TotalCost { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? FinishedAt { get; private set; }
    public ProjectStatusEnum Status { get; private set; }
    public List<ProjectComment> Comments { get; private set; }

    public bool SetAndReturnIfCanCancel()
    {
        if (Status is ProjectStatusEnum.Created || Status is ProjectStatusEnum.InProgress)
        {
            Status = ProjectStatusEnum.Cancelled;
            return true;
        }

        return false;
    }

    public bool SetAndReturnIfCanStart()
    {
        if (Status is ProjectStatusEnum.Created)
        {
            Status = ProjectStatusEnum.InProgress;
            StartedAt = DateTime.Now;
            return true;
        }

        return false;
    }

    public bool SetAndReturnIfCanFinish()
    {
        if (Status is ProjectStatusEnum.InProgress)
        {
            Status = ProjectStatusEnum.Finished;
            FinishedAt = DateTime.Now;
            return true;
        }

        return false;
    }

    public void Update(string title, string description, decimal totalCost)
    {
        Title = title;
        Description = description;
        TotalCost = totalCost;
    }
}
