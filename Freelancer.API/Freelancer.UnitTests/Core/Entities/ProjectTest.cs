using Freelancer.Core.Entities;
using Freelancer.Core.Enums;

namespace Freelancer.UnitTests.Core.Entities;
public class ProjectTest
{
    [Fact]
    public void TestIfProjectStartWorks()
    {
        var project = new Project("Titulo Projeto 1", "Descrição projeto 1", 1, 2, 1000);

        Assert.Equal(ProjectStatusEnum.Created, project.Status);
        Assert.Null(project.StartedAt);

        var canStart = project.SetAndReturnIfCanStart();

        Assert.True(canStart);
        Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
        Assert.NotNull(project.StartedAt);
    }
}
