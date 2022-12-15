using AutoMapper;
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

    
}
