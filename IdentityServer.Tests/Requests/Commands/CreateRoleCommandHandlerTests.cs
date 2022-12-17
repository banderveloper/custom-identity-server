using IdentityServer.Application.Common.Exceptions;
using IdentityServer.Application.Requests.Commands.SaCreateRole;
using IdentityServer.Domain.IdentityUser;
using IdentityServer.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace IdentityServer.Tests.Requests.Commands;

public class CreateRoleCommandHandlerTests : TestBase
{
    [Fact]
    public async Task CreateRoleCommandHandler_Success()
    {
        // Arrange
        var handler = new SaCreateRoleCommandHandler(Context);
        var newRoleName = "testRole";
        
        // Act
        await handler.Handle(new SaCreateRoleCommand
        {
            RoleName = newRoleName
        }, CancellationToken.None);

        var createdRole = await Context.Roles
            .FirstOrDefaultAsync(role => role.Name == newRoleName.ToLower());

        // Assert
        createdRole.ShouldBeOfType<IdentityUserRole?>();
        createdRole.Id.ShouldBeGreaterThan(0);
        createdRole.Name.ShouldBe(newRoleName.ToLower());
    }

    [Fact]
    public async Task CreateRoleCommandHandler_FailOnExistingRole()
    {
        // Arrange
        var handler = new SaCreateRoleCommandHandler(Context);
        
        // Act
        // Assert
        await Assert.ThrowsAsync<AlreadyExistsException>(async () =>
        {
            await handler.Handle(new SaCreateRoleCommand
            {
                RoleName = RoleConfiguration.UserRole
            }, CancellationToken.None);
        });
    }
}