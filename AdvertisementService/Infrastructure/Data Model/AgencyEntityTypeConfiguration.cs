using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataModel
{
    internal class AgencyEntityTypeConfiguration : BaseEntityTypeConfiguration, IEntityTypeConfiguration<Agency>
    {
        public void Configure(EntityTypeBuilder<Agency> builder)
        {
            ConfigureNamingConventions(builder);
        }
    }
}
