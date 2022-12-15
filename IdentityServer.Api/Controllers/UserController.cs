using AutoMapper;
using IdentityServer.Api.Models;
using IdentityServer.Application.Requests.Commands.CreateUser;
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
        // map model to CQRS command for mediator
        var createCommand = _mapper.Map<CreateUserCommand>(model);

        // created user id
        var userId = await _mediator.Send(createCommand);

        return Ok(new
        {
            IsSucceed = true,
            UserId = userId
        });
    }
}