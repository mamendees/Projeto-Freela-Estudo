using Freelancer.Application.Commands.ProjectCommands.CreateProject;
using Freelancer.Core.Entities;
using Freelancer.Core.Repositories;
using NSubstitute;

namespace Freelancer.UnitTests.Application.Commands.ProjectCommands;
public class CreateProjectCommandHandlerTestes
{
    private readonly IProjectRepository _projectRepositoryMock;
    public CreateProjectCommandHandlerTestes()
    {
        _projectRepositoryMock = Substitute.For<IProjectRepository>();
    }

    [Fact]
    public async Task InputDataIsOk_Excuted_ReturnProjectId()
    {
        //Arrange
        var createProjectCommandHandler = new CreateProjectCommandHandler(_projectRepositoryMock);

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
        await _projectRepositoryMock.Received(1).AddAsync(Arg.Any<Project>());
        Assert.IsType<int>(idRetorno);
        Assert.True(idRetorno >= 0);
    }
}
