using AutoMapper;
using IdentityServer.Api.Models;
using IdentityServer.Application.Requests.Commands.CreateUser;
using IdentityServer.Application.Requests.Queries.GetUserPublicData;
using IdentityServer.Application.Requests.Queries.GetUserToken;
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

    [HttpGet("token/get/{username}/{password}")]
    public async Task<IActionResult> GetToken(string username, string password)
    {
        // create query for getting access token
        var query = new GetUserTokenQuery
        {
            Username = username,
            Password = password
        };

        // send query and get access token
        var token = await _mediator.Send(query);

        return Ok(new { accessToken = token });
    }

    [HttpGet("userinfo/{username}")]
    public async Task<IActionResult> GetUserInfo(string username)
    {
        // create query for getting user public data
        var query = new GetUserPublicDataQuery
        {
            Username = username
        };

        // send query and get public data
        var publicData = await _mediator.Send(query);

        return Ok(publicData);
    }
}