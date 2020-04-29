using ApplicationCore.Models;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DataModel
{
    class LocationEntityTypeConfiguration : BaseEntityTypeConfiguration, IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            ConfigureNamingConventions(builder);
        }
    }
}
