using Freelancer.Application.Queries.ProjectQueries.GetProjectById;
using Freelancer.Application.ViewModels;
using Freelancer.Core.Entities;
using Freelancer.Core.Repositories;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Freelancer.UnitTests.Application.Queries.ProjectQueries;
public class GetProjectByIdTestes
{
    private readonly IProjectRepository _projectRepositoryMock;
    public GetProjectByIdTestes()
    {
        _projectRepositoryMock = Substitute.For<IProjectRepository>();  
    }

    [Fact]
    public async Task IdProjectDoesntExist_Executed_ReturnNull()
    {
        //Arrange
        var getProjectByIdQuery = new GetProjectByIdQuery(1);
        _projectRepositoryMock.GetByIdAsync(getProjectByIdQuery.Id).ReturnsNull();

        var getProjectByIdQueryHandler = new GetProjectByIdQueryHandler(_projectRepositoryMock);

        //Act
        var projectReturn = await getProjectByIdQueryHandler.Handle(getProjectByIdQuery, new CancellationToken());

        //Assert
        Assert.Null(projectReturn);
        await _projectRepositoryMock.Received(1).GetByIdAsync(getProjectByIdQuery.Id);
    }

    [Fact]
    public async Task IdProjectExist_Executed_ReturnProjectDetailsViewModel()
    {
        //Arrange
        var getProjectByIdQuery = new GetProjectByIdQuery(2);
        var project = new Project("Title 1", "Desc 1 ", 1, 2, 1000, 
            new User("Client", "", DateTime.Now, "", ""), 
            new User("Freelancer", "", DateTime.Now, "", ""));

        _projectRepositoryMock.GetByIdAsync(getProjectByIdQuery.Id).Returns(project);

        var getProjectByIdQueryHandler = new GetProjectByIdQueryHandler(_projectRepositoryMock);

        //Act
        var projectReturn = await getProjectByIdQueryHandler.Handle(getProjectByIdQuery, new CancellationToken());

        //Assert
        Assert.NotNull(projectReturn);
        Assert.IsType<ProjectDetailsViewModel?>(projectReturn);
        Assert.Equal(project.Title, projectReturn.Title);
        Assert.Equal("Client", projectReturn.ClientFullName);
        Assert.Equal("Freelancer", projectReturn.FreelancerFullName);
        await _projectRepositoryMock.Received(1).GetByIdAsync(getProjectByIdQuery.Id);
    }
}
