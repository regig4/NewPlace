using ApplicationCore.Models;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DataModel
{
    class UserEntityTypeConfiguration : BaseEntityTypeConfiguration, IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            ConfigureNamingConventions(builder);
            Required(builder, nameof(User.Login), nameof(User.PasswordHash));
            builder.Property(u => u.PasswordHash).HasMaxLength(512);
        }
    }
}
