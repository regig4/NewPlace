using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataModel
{
    internal class EstateEntityTypeConfiguration : BaseEntityTypeConfiguration, IEntityTypeConfiguration<Estate>
    {
        public void Configure(EntityTypeBuilder<Estate> builder)
        {
            builder.ToTable(nameof(Estate));
            ConfigureNamingConventions(builder);
        }
    }
}
