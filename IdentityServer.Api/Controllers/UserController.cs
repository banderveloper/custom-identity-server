using AutoMapper;
using IdentityServer.Api.Models;
using IdentityServer.Application.Requests.Commands.CreateUser;
using IdentityServer.Application.Requests.Queries.GetUserPublicData;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Api.Controllers;

// Default controller
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UserController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterModel model)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        
        // map model to CQRS command for mediator
        var createCommand = _mapper.Map<CreateUserCommand>(model);

        // public data of created user
        var publicData = await _mediator.Send(createCommand);

        return Ok(publicData);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginModel model)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        
        var query = _mapper.Map<GetUserPublicDataQuery>(model);
        var publicData = await _mediator.Send(query);

        return Ok(publicData);
    }
}