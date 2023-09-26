using Dapper;
using Freelancer.Core.Entities;
using Freelancer.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Freelancer.Infrastructure.Persistence.Repositories;
public class SkillRepository : ISkillRepository
{
    private readonly string _connectionString;
    private readonly FreelancerDbContext _context;

    public SkillRepository(IConfiguration configuration, FreelancerDbContext context)
    {
        _connectionString = configuration.GetConnectionString("FreelancerCs") ?? throw new ArgumentNullException("FreelancerCs");
        _context = context;
    }

    //Metodo sera excluido, apenas teste UOW
    public async Task AddSkillFromProject(Project project)
    {
        var skill = $"{project.Id} - Teste";
        await _context.Skills.AddAsync(new Skill(skill));
    }

    public async Task<List<Skill>> GetAllAsync()
    {
        using (var sqlConnection = new SqlConnection(_connectionString))
        {
            var script = "SELECT * FROM Skills";
            return (await sqlConnection.QueryAsync<Skill>(script)).ToList();
        }
    }
}
