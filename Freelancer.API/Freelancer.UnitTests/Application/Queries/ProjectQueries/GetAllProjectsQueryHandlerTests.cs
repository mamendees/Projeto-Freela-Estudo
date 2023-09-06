using Freelancer.Application.Queries.ProjectQueries.GetAllProjects;
using Freelancer.Core.Entities;
using Freelancer.Core.Repositories;
using NSubstitute;
using NSubstitute.ReceivedExtensions;

namespace Freelancer.UnitTests.Application.Queries.ProjectQueries;
public class GetAllProjectsQueryHandlerTests
{
    private readonly IProjectRepository _projectRepositoryMock;
    public GetAllProjectsQueryHandlerTests()
    {
        _projectRepositoryMock = Substitute.For<IProjectRepository>();
    }

    [Fact]
    public async Task ThreeProjectsExist_Executed_ReturnThreeProjectViewModels()
    {
        //Arrange
        var projects = new List<Project>
        {
            new Project("Title 1", "Desc 1 ", 1, 2, 1000),
            new Project("Title 2", "Desc 2 ", 1, 2, 2000),
            new Project("Title 3", "Desc 3 ", 1, 2, 3000)
        };

        _projectRepositoryMock.GetAllAsync().Returns(projects);

        var getAllProjectQuery = new GetAllProjectsQuery("");
        var getAllProjectQueryHandler = new GetAllProjectsQueryHandler(_projectRepositoryMock);

        //Act
        var projectViewModelList = await getAllProjectQueryHandler.Handle(getAllProjectQuery, new CancellationToken());

        //Assert
        Assert.NotNull(projectViewModelList);
        Assert.NotEmpty(projectViewModelList);
        Assert.Equal(3, projectViewModelList.Count);
        await _projectRepositoryMock.Received(1).GetAllAsync();
    }
}
