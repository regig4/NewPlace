using ApplicationCore.Models;
using Infrastructure.DataModel;
using Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DataModel
{
    class CategoryEntityTypeConfiguration : BaseEntityTypeConfiguration, IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            ConfigureNamingConventions(builder);
        }
    }
}
