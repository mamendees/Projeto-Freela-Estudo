using Freelancer.Application.Queries.ProjectQueries.GetAllProjects;
using Freelancer.Core.Entities;
using Freelancer.Core.Models;
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
            new Project("Title um", "Desc 1 ", 1, 2, 1000),
            new Project("Title dois", "Desc 2 ", 1, 2, 2000),
            new Project("Title tres", "Desc 3 ", 1, 2, 3000)
        };

        int page = 1;
        int pageSize = 2;

        var paginationResult = new PaginationResult<Project>()
        {
            Page = page,
            PageSize = pageSize,
            ItemsCount = 3,
            TotalPages = 2,
            Data = projects
        };

        var getAllProjectQuery = new GetAllProjectsQuery()
        {
            PageSize = pageSize,
            Page = page,
            Query = null
        };

        _projectRepositoryMock.GetAllAsync(getAllProjectQuery.Query, getAllProjectQuery.Page, getAllProjectQuery.PageSize).Returns(paginationResult);

        var getAllProjectQueryHandler = new GetAllProjectsQueryHandler(_projectRepositoryMock);

        //Act
        var paginationResultProjectViewModelList = await getAllProjectQueryHandler.Handle(getAllProjectQuery, new CancellationToken());

        //Assert
        Assert.NotNull(paginationResultProjectViewModelList);
        Assert.NotEmpty(paginationResultProjectViewModelList.Data);
        Assert.Equal(3, paginationResultProjectViewModelList.Data.Count);
        Assert.Equal(paginationResult.Page, paginationResultProjectViewModelList.Page);
        Assert.Equal(paginationResult.PageSize, paginationResultProjectViewModelList.PageSize);
        Assert.Equal(paginationResult.TotalPages, paginationResultProjectViewModelList.TotalPages);
        Assert.Equal(paginationResult.ItemsCount, paginationResultProjectViewModelList.ItemsCount);
        await _projectRepositoryMock.Received(1).GetAllAsync(getAllProjectQuery.Query, Arg.Any<int>(), getAllProjectQuery.PageSize);
    }
}
