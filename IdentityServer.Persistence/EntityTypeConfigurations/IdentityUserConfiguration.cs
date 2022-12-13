using IdentityServer.Domain.IdentityUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Persistence.EntityTypeConfigurations;

// Fluent API configuration for IdentityUser
public class IdentityUserConfiguration : IEntityTypeConfiguration<IdentityUser>
{
    public void Configure(EntityTypeBuilder<IdentityUser> builder)
    {
        // id
        builder.HasKey(p => p.Id);

        // username
        builder.Property(p => p.Username).HasMaxLength(24);
        builder.Property(p => p.Username).IsRequired();
        builder.HasIndex(p => p.Username).IsUnique();

        // email
        builder.Property(p => p.Email).IsRequired();
        builder.HasIndex(p => p.Email).IsUnique();

        // password
        builder.Property(p => p.PasswordHash).IsRequired();

        // personal
        builder.HasOne(user => user.Personal)
            .WithOne(personal => personal.User)
            .HasForeignKey<IdentityUserPersonal>(personal => personal.UserId);

        // role
        builder.HasOne(user => user.Role)
            .WithMany(role => role.Users)
            .HasForeignKey(user => user.RoleId);
    }
}