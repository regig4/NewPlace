using ApplicationCore.Models;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DataModel
{
    class EstateEntityTypeConfiguration : BaseEntityTypeConfiguration, IEntityTypeConfiguration<Estate>
    {
        public void Configure(EntityTypeBuilder<Estate> builder)
        {
            builder.ToTable(nameof(Estate));
            ConfigureNamingConventions(builder);
        }
    }
}
