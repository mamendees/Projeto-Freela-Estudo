using Freelancer.Core.Entities;
using Freelancer.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Freelancer.Infrastructure.Persistence.Repositories;
public class UserRepository : IUserRepository
{
    private readonly FreelancerDbContext _dbContext;
    public UserRepository(FreelancerDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User?> GetUserByEmailAndPasswordAsync(string email, string passwordHash)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(user => user.Email.Equals(email) && user.Password.Equals(passwordHash));
    }
}
