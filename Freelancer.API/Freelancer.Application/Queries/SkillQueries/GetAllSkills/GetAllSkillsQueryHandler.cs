using Freelancer.Application.ViewModels;
using Freelancer.Core.Repositories;
using MediatR;

namespace Freelancer.Application.Queries.SkillQueries.GetAllSkills;
public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, List<SkillViewModel>>
{
    private readonly ISkillRepository _skillRepository;
    public GetAllSkillsQueryHandler(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task<List<SkillViewModel>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
    {
        var skills = await _skillRepository.GetAllAsync();
        return skills.Select(skilll => new SkillViewModel(skilll.Id, skilll.Description)).ToList();
    }
}
