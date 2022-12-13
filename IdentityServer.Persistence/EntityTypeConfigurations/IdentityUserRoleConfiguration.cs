using IdentityServer.Domain.IdentityUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Persistence.EntityTypeConfigurations;

public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole> builder)
    {
        // id
        builder.HasKey(role => role.Id);
        
        // name
        builder.HasIndex(role => role.Name).IsUnique();
    }
}