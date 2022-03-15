using ApplicationCore.Models;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data_Model
{
    class CountryEntityTypeConfiguration : BaseEntityTypeConfiguration, IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            ConfigureNamingConventions(builder);
        }
    }
}
