using Freelancer.Core.Entities;
using Freelancer.Core.Repositories;
using MediatR;

namespace Freelancer.Application.Commands.ProjectCommands.CreateProject;
public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateProjectCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Project(request.Title, request.Description, request.IdClient, request.IdFreelancer, request.TotalCost);

        await _unitOfWork.BeginTranscationAsync();
        await _unitOfWork.ProjectRepository.AddAsync(project);
        await _unitOfWork.CompleteAsync();
        await _unitOfWork.SkillRepository.AddSkillFromProject(project);
        await _unitOfWork.CompleteAsync();
        await _unitOfWork.CommitAsync();

        return project.Id;
    }
}
