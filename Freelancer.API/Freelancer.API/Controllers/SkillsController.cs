using Freelancer.Application.Queries.SkillQueries.GetAllSkills;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Freelancer.API.Controllers;
[ApiController]
[Route("api/skills")]
public class SkillsController : ControllerBase
{
    private readonly IMediator _mediator;
    public SkillsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var query = new GetAllSkillsQuery();
        var skills = await _mediator.Send(query);
        return Ok(skills);
    }
}
