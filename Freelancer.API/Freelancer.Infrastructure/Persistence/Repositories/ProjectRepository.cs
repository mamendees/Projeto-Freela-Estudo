using Freelancer.Core.Entities;
using Freelancer.Core.Models;
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

    public async Task<PaginationResult<Project>> GetAllAsync(string? query, int page, int pageSize)
    {
        var project = _dbContext.Projects.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query))
        {
            project = project.Where(p => p.Title.Contains(query));
        }

        return await project.GetPaged(page, pageSize);
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
    }

    public async Task CreateCommentAsync(ProjectComment projectComment)
    {
        await _dbContext.ProjectComments.AddAsync(projectComment);
    }
}
