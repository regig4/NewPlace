using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataModel
{
    internal class UtilityEntityTypeConfiguration : BaseEntityTypeConfiguration, IEntityTypeConfiguration<Utility>
    {
        public void Configure(EntityTypeBuilder<Utility> builder)
        {
            ConfigureNamingConventions(builder);
        }
    }
}
