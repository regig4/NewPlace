using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentService.ApplicationCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DataModel
{
    class UserTypeConfiguration : BaseEntityTypeConfiguration, IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigureNamingConventions(builder);
            builder.OwnsOne(p => p.PointsValue, sa =>
            {
                sa.Property(p => p.Amount).HasColumnName("points_amount");
            });
        }
    }
}
