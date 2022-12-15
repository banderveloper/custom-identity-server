using AutoMapper;
using IdentityServer.Application.Common.Exceptions;
using IdentityServer.Application.Common.Services;
using IdentityServer.Application.Interfaces;
using IdentityServer.Domain.IdentityUser;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Application.Requests.Queries.GetUserPublicData;

public class GetUserPublicDataQueryHandler : IRequestHandler<GetUserPublicDataQuery, UserPublicDataDto>
{
    private readonly IIdentityDbContext _context;
    private readonly IMapper _mapper;

    public GetUserPublicDataQueryHandler(IIdentityDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserPublicDataDto> Handle(GetUserPublicDataQuery request, CancellationToken cancellationToken)
    {
        // try to get user with given username
        var user = await GetUserByUsernameAsync(request.Username, cancellationToken);

        // If user not found - throw exception
        if (user is null)
            throw new NotFoundException(nameof(user), request.Username);

        // If auth ok - map to public data and return
        var publicData = _mapper.Map<UserPublicDataDto>(user);

        return publicData;
    }

    private async Task<IdentityUser?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(user => user.Personal)
            .Include(user => user.Role)
            .FirstOrDefaultAsync(user => user.Username == username,
                cancellationToken);
    }
}