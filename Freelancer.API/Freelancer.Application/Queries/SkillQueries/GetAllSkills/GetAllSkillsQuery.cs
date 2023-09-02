using Freelancer.Application.ViewModels;
using MediatR;

namespace Freelancer.Application.Queries.SkillQueries.GetAllSkills;
public class GetAllSkillsQuery : IRequest<List<SkillViewModel>>
{
}
