using IdentityServer.Application.Common.Configurations;
using IdentityServer.Application.Common.Exceptions;
using IdentityServer.Application.Common.Services;
using IdentityServer.Application.Requests.Commands.CreateUser;
using IdentityServer.Domain.IdentityUser;
using IdentityServer.Persistence;
using IdentityServer.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace IdentityServer.Tests.Requests.Commands;

public class CreateUserCommandHandlerTests : TestBase
{
    [Fact]
    public async Task CreateUserCommandHandler_Success()
    {
        // Arrange
        var handler = new CreateUserCommandHandler(Context, RoleConfiguration);

        var username = "superUsername";
        var password = "superPassword";
        var email = "superEmail@gmail.com";

        // Act
        await handler.Handle(new CreateUserCommand
        {
            Username = username,
            Password = password,
            Email = email
        }, CancellationToken.None);

        var createdUser = await Context.Users
            .FirstOrDefaultAsync(user => user.Username == username);

        // Assert
        createdUser.ShouldBeOfType<IdentityUser>();
        createdUser.Username.ShouldBe(username);
        createdUser.PasswordHash.ShouldBe(Sha256.Hash(password));
        createdUser.Personal.Email.ShouldBe(email);
        createdUser.Role.Name.ShouldBe(RoleConfiguration.UserRole);
    }

    [Fact]
    public async Task CreateUserCommandHandler_FailOnExistingUsername()
    {
        // Arrange
        var handler = new CreateUserCommandHandler(Context, RoleConfiguration);

        // Act
        // Assert
        await Assert.ThrowsAsync<AlreadyExistsException>(async () =>
        {
            await handler.Handle(new CreateUserCommand
            {
                Username = IdentityContextFactory.UsernameA,
                Password = "notValuable",
            }, CancellationToken.None);
        });
    }
}