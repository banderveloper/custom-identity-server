using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using IdentityServer.Api.Models.SA;
using IdentityServer.Application.Requests.SA.Commands.CreateRole;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SaController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public SaController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("createRole")]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleModel model)
    {
        var command = _mapper.Map<CreateRoleCommand>(model);
        await _mediator.Send(command);

        return NoContent();
    }
}
