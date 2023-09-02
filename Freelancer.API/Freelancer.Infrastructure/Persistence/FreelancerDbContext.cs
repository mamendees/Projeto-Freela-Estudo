using Freelancer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Freelancer.Infrastructure.Persistence;
public class FreelancerDbContext : DbContext
{
    public FreelancerDbContext(DbContextOptions<FreelancerDbContext> options) : base(options)
    {
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<UserSkill> UserSkills { get; set; }
    public DbSet<ProjectComment> ProjectComments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
