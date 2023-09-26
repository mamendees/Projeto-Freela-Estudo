using Freelancer.Core.Entities;
using Freelancer.Core.Models;

namespace Freelancer.Core.Repositories;
public interface IProjectRepository
{
    Task SaveChangesAsync();
    Task<PaginationResult<Project>> GetAllAsync(string? query, int page = 1, int pageSize = 10);
    Task<Project?> GetByIdAsync(int id);
    Task AddAsync(Project project);
    Task CreateCommentAsync(ProjectComment projectComment);
}
