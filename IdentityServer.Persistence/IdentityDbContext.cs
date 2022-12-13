using IdentityServer.Application.Interfaces;
using IdentityServer.Domain.IdentityUser;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Persistence;

public class IdentityDbContext : DbContext, IIdentityDbContext
{
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityUserRole> Roles { get; set; }
    public DbSet<IdentityUserPersonal> Personals { get; set; }

    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // apply configurations
    }
}