using AutoMapper;
using IdentityServer.Api.Models.User;
using IdentityServer.Application.Requests.User.Commands.RegisterUser;
using IdentityServer.Application.Requests.User.Queries.GetUserJwt;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Api.Controllers;

// Default controller
[ApiController]
public class UserController
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UserController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    
    // Register user and return his jwt
    [HttpPost("register")]
    public async Task<ActionResult<string>> RegisterUser([FromBody] RegisterUserModel model)
    {
        // register user and return his id for getting jwt
        var registerUserCommand = _mapper.Map<RegisterUserCommand>(model);
        var registeredUserId = await _mediator.Send(registerUserCommand);

        // get user jwt token
        var getUserJwtQuery = new GetUserJwtQuery() { UserId = registeredUserId };
        var userToken = await _mediator.Send(getUserJwtQuery);
        
        // return user jwt
        return userToken;
    }
}