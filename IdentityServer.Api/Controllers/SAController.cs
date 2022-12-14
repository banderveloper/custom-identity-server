using System.Diagnostics.CodeAnalysis;
using IdentityServer.Application.Requests.SA.Commands.CreateRole;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SAController : ControllerBase
{
    private readonly IMediator _mediator;

    public SAController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("You are SA!");
    }

    // [HttpPost("createRole")]
    // public async Task<IActionResult> CreateRole([FromBody] CreateRoleModel model)
    // {
    //     await _mediator.Send(new CreateRoleCommand() { Name = model.Name });
    //     return Ok();
    // }
}
