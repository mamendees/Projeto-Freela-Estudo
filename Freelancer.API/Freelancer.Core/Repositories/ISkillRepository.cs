using Freelancer.Core.Entities;
namespace Freelancer.Core.Repositories;
public interface ISkillRepository
{
    Task<List<Skill>> GetAllAsync();
}
