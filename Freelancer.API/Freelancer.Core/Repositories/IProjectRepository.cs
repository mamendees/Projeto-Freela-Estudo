using Freelancer.Core.Entities;
using Freelancer.Core.Enums;

namespace Freelancer.Core.Repositories;
public interface IProjectRepository
{
    Task SaveChangesAsync();
    Task<List<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(int id);
    Task AddAsync(Project project);
    Task CreateCommentAsync(ProjectComment projectComment);
}
