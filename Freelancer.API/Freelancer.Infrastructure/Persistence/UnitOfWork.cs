
using Freelancer.Core.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Freelancer.Infrastructure.Persistence;
public class UnitOfWork : IUnitOfWork
{
    public IProjectRepository ProjectRepository { get; }
    public IUserRepository UserRepository { get; }
    public ISkillRepository SkillRepository { get; }

    private readonly FreelancerDbContext _context;
    private IDbContextTransaction _transaction;
   
    public UnitOfWork(
        FreelancerDbContext freelancerDbContext,
        IProjectRepository projectRepository,
        IUserRepository userRepository,
        ISkillRepository skillRepository)
    {
        _context = freelancerDbContext;
        ProjectRepository = projectRepository;
        UserRepository = userRepository;
        SkillRepository = skillRepository;
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTranscationAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        try
        {
            await _transaction.CommitAsync();
        }
        catch (Exception)
        {
            await _transaction.RollbackAsync();
            throw;
        }
    }
}
