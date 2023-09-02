using Freelancer.Core.Entities;
using Freelancer.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Freelancer.Infrastructure.Persistence.Repositories;
public class ProjectRepository : IProjectRepository
{
    private readonly FreelancerDbContext _dbContext;
    public ProjectRepository(FreelancerDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Project>> GetAllAsync()
    {
        return await _dbContext.Projects.ToListAsync();
    }

    public async Task<Project?> GetByIdAsync(int id)
    {
        return await _dbContext.Projects
           .Include(p => p.Client)
           .Include(p => p.Freelancer)
           .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Project project)
    {
        await _dbContext.Projects.AddAsync(project);
        await SaveChangesAsync();
    }

    public async Task CreateCommentAsync(ProjectComment projectComment)
    {
        await _dbContext.ProjectComments.AddAsync(projectComment);
        await SaveChangesAsync();
    }
}
