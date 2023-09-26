using Freelancer.Application.Commands.ProjectCommands.CreateProject;
using Freelancer.Core.Entities;
using Freelancer.Core.Repositories;
using NSubstitute;

namespace Freelancer.UnitTests.Application.Commands.ProjectCommands;
public class CreateProjectCommandHandlerTestes
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProjectRepository _projectRepository;
    private readonly ISkillRepository _skillRepository;
    public CreateProjectCommandHandlerTestes()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _projectRepository = Substitute.For<IProjectRepository>();
        _skillRepository = Substitute.For<ISkillRepository>();
    }

    [Fact]
    public async Task InputDataIsOk_Excuted_ReturnProjectId()
    {
        //Arrange
        var createProjectCommandHandler = new CreateProjectCommandHandler(_unitOfWork);
        _unitOfWork.ProjectRepository.Returns(_projectRepository);
        _unitOfWork.SkillRepository.Returns(_skillRepository);

        var createProjectCommand = new CreateProjectCommand
        {
            Title = "Title 1",
            Description = "Description 2",
            IdClient = 1,
            IdFreelancer = 2,
            TotalCost = 1000
        };

        //Act
        var idRetorno = await createProjectCommandHandler.Handle(createProjectCommand, new CancellationToken());

        //Assert
        await _unitOfWork.Received(1).ProjectRepository.AddAsync(Arg.Any<Project>());
        await _unitOfWork.Received(1).SkillRepository.AddSkillFromProject(Arg.Any<Project>());
        await _unitOfWork.Received(2).CompleteAsync();
        await _unitOfWork.Received(1).CommitAsync();
        await _projectRepository.Received(1).AddAsync(Arg.Any<Project>());
        await _skillRepository.Received(1).AddSkillFromProject(Arg.Any<Project>());
        Assert.IsType<int>(idRetorno);
        Assert.True(idRetorno >= 0);
    }
}
