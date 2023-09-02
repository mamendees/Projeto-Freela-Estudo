using Dapper;
using Freelancer.Core.Entities;
using Freelancer.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Freelancer.Infrastructure.Persistence.Repositories;
public class SkillRepository : ISkillRepository
{
    private readonly string _connectionString;
    public SkillRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("FreelancerCs") ?? throw new ArgumentNullException("FreelancerCs");
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
