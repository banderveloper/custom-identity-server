﻿using IdentityServer.Domain.IdentityUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Persistence.EntityTypeConfigurations;

// Fluent API configuration 
public class IdentityUserPersonalConfiguration : IEntityTypeConfiguration<IdentityUserPersonal>
{
    public void Configure(EntityTypeBuilder<IdentityUserPersonal> builder)
    {
        // id
        builder.HasKey(personal => personal.Id);

        // first name
        builder.Property(personal => personal.FirstName).HasMaxLength(32);
        
        // last name
        builder.Property(personal => personal.LastName).HasMaxLength(32);
        
        // phone number
        builder.Property(personal => personal.PhoneNumber).HasMaxLength(16);
        
        // email
        builder.Property(p => p.Email).HasMaxLength(64);
        
        // country
        builder.Property(p => p.Country).HasMaxLength(32);
        
        // age
        
        // work
        builder.Property(p => p.Work).HasMaxLength(128);
        
        // work post
        builder.Property(p => p.WorkPost).HasMaxLength(128);
        
        // user
        builder.HasOne(personal => personal.User)
            .WithOne(user => user.Personal)
            .HasForeignKey<IdentityUser>(user => user.PersonalId);
    }
}