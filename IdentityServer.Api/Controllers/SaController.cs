using AutoMapper;
using IdentityServer.Api.Models;
using IdentityServer.Application.Requests.Commands.SaCreateRole;
using IdentityServer.Application.Requests.Queries.SaGetUserData;
using IdentityServer.Domain.IdentityUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IdentityServer.Api.Controllers;

[ApiController]
[Route("sa")]
public class SaController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public SaController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("userInfo/{username}")]
    public async Task<IActionResult> GetUserFullInfo(string username)
    {
        // sa query for getting full user info including id and password hash 
        var query = new SaGetUserDataQuery { Username = username };

        // get identity user
        var user = await _mediator.Send(query);
        
        // map to model without json ignore if role and personals
        var userInfo = _mapper.Map<GetUserFullInfoModel>(user);

        return Ok(userInfo);
    }

    [HttpPost("createRole")]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleModel model)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        // map model to cqrs command
        var command = _mapper.Map<SaCreateRoleCommand>(model);
        // send command which creates role
        await _mediator.Send(command);

        return NoContent();
    }
}