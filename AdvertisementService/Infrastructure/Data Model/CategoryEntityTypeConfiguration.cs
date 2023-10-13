using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataModel
{
    internal class CategoryEntityTypeConfiguration : BaseEntityTypeConfiguration, IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            ConfigureNamingConventions(builder);
        }
    }
}
