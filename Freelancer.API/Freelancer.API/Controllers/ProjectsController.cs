using Freelancer.Application.Commands.ProjectCommands.CreateComment;
using Freelancer.Application.Commands.ProjectCommands.CreateProject;
using Freelancer.Application.Commands.ProjectCommands.DeleteProject;
using Freelancer.Application.Commands.ProjectCommands.FinishProject;
using Freelancer.Application.Commands.ProjectCommands.StartProject;
using Freelancer.Application.Commands.ProjectCommands.UpdateProject;
using Freelancer.Application.Queries.ProjectQueries.GetAllProjects;
using Freelancer.Application.Queries.ProjectQueries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Freelancer.API.Controllers;
[ApiController]
[Authorize]
[Route("api/projects")]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "client, freelancer")]
    public async Task<IActionResult> GetAsync(string query)
    {
        var getAllProjectsQuery = new GetAllProjectsQuery(query);
        var projects = await _mediator.Send(getAllProjectsQuery);
        return Ok(projects);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "client, freelancer")]
    [ActionName(nameof(GetByIdAsync))]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var query = new GetProjectByIdQuery(id);
        var project = await _mediator.Send(query);

        if (project == null)
        {
            return NotFound();
        }

        return Ok(project);
    }

    [HttpPost]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> PostAsync([FromBody] CreateProjectCommand command)
    {
        if (command.Title.Length > 50)
        {
            return BadRequest();
        }

        int id = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetByIdAsync), new { id }, command);
    }

    [HttpPut]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> PutAsync([FromBody] UpdateProjectCommand command)
    {
        if (command.Description.Length > 200)
        {
            return BadRequest();
        }

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var command = new DeleteProjectCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("comments")]
    [Authorize(Roles = "client, freelancer")]
    public async Task<IActionResult> PostCommentAsync([FromBody] CreateCommentCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id}/start")]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> StartAsync(int id)
    {
        var command = new StartProjectCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id}/finish")]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> FinishAsync(int id)
    {
        var command = new FinishProjectCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}
