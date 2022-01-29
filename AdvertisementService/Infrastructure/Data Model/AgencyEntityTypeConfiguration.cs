using ApplicationCore.Models;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DataModel
{
    class AgencyEntityTypeConfiguration : BaseEntityTypeConfiguration, IEntityTypeConfiguration<Agency>
    {
        public void Configure(EntityTypeBuilder<Agency> builder)
        {
            ConfigureNamingConventions(builder);
        }
    }
}
