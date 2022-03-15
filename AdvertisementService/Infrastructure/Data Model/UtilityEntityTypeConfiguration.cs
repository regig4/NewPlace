using ApplicationCore.Models;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DataModel
{
    class UtilityEntityTypeConfiguration : BaseEntityTypeConfiguration, IEntityTypeConfiguration<Utility>
    {
        public void Configure(EntityTypeBuilder<Utility> builder)
        {
            ConfigureNamingConventions(builder);
        }
    }
}
