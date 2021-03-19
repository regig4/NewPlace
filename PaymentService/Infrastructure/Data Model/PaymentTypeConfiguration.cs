using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentService.ApplicationCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DataModel
{
    class PaymentTypeConfiguration : BaseEntityTypeConfiguration, IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            ConfigureNamingConventions(builder);
            builder.OwnsOne(p => p.MoneyValue);
            builder.OwnsOne(p => p.PointsValue);
        }
    }
}
