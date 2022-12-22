using IdentityServer.Application.Common.Exceptions;
using IdentityServer.Application.Requests.Commands.SaChangeUserRole;
using IdentityServer.Application.Requests.Commands.SaCreateRole;
using IdentityServer.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace IdentityServer.Tests.Requests.Commands;

public class ChangeUserRoleCommandHandlerTests : TestBase
{
    [Fact]
    public async Task ChangeUserRoleCommandHandler_Success()
    {
        // Arrange
        var changeRoleHandler = new SaChangeUserRoleCommandHandler(Context);
        var createRoleHandler = new SaCreateRoleCommandHandler(Context);
        var newRole = "jocker";

        // Act
        await createRoleHandler.Handle(new SaCreateRoleCommand()
        {
            RoleName = newRole
        }, CancellationToken.None);

        await changeRoleHandler.Handle(new SaChangeUserRoleCommand()
        {
            Username = IdentityContextFactory.UsernameA,
            NewRoleName = newRole
        }, CancellationToken.None);

        var userWithChangedRole =
            await Context.Users.FirstOrDefaultAsync(user => user.Username == IdentityContextFactory.UsernameA);

        // Assert
        Assert.NotNull(userWithChangedRole);
        userWithChangedRole.Role.Name.ShouldBe(newRole);
    }

    [Fact]
    public async Task ChangeUserRoleCommandHandler_FailOnWrongRoleName()
    {
        // Arrange
        var changeRoleHandler = new SaChangeUserRoleCommandHandler(Context);
        
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await changeRoleHandler.Handle(new SaChangeUserRoleCommand()
            {
                Username = IdentityContextFactory.UsernameA,
                NewRoleName = "NON_EXISTING_ROLE"
            }, CancellationToken.None);
        });
    }
    
    [Fact]
    public async Task ChangeUserRoleCommandHandler_FailOnWrongUserName()
    {
        // Arrange
        var changeRoleHandler = new SaChangeUserRoleCommandHandler(Context);
        
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await changeRoleHandler.Handle(new SaChangeUserRoleCommand()
            {
                Username = "NON_EXISTING_USERNAME",
                NewRoleName = "user"
            }, CancellationToken.None);
        });
    }
}