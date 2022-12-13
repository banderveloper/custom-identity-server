using IdentityServer.Domain.IdentityUser;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Application.Interfaces;

// Interface of main database context
public interface IIdentityDbContext
{
    DbSet<IdentityUser> Users { get; set; }
    DbSet<IdentityUserRole> Roles { get; set; }
    DbSet<IdentityUserPersonal> Personals { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}