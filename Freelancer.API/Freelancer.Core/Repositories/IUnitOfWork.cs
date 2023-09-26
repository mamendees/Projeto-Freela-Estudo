
namespace Freelancer.Core.Repositories;
public interface IUnitOfWork
{
    IProjectRepository ProjectRepository { get; }
    IUserRepository UserRepository { get; }
    ISkillRepository SkillRepository { get; }

    Task<int> CompleteAsync();
    Task BeginTranscationAsync();
    Task CommitAsync();
}
