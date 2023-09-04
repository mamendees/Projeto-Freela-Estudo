using Freelancer.Application.Commands.UserCommands.CreateUser;
using Freelancer.Application.Commands.UserCommands.LoginUser;
using Freelancer.Application.Queries.UserQueries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Freelancer.API.Controllers;
[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [ActionName(nameof(GetByIdAsync))]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var query = new GetUserByIdQuery(id);
        var user = await _mediator.Send(query);
        if (user is null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> PostAsync([FromBody] CreateUserCommand command)
    {
        int id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetByIdAsync), new { id }, command);
    }

    [HttpPut("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var usuario = await _mediator.Send(command);
        if (usuario is null) return BadRequest();

        return Ok(usuario);
    }
}
