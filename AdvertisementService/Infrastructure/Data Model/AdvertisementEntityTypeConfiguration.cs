using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataModel
{
    internal class AdvertisementEntityTypeConfiguration : BaseEntityTypeConfiguration, IEntityTypeConfiguration<Advertisement>
    {
        public void Configure(EntityTypeBuilder<Advertisement> builder)
        {
            builder.ToTable(nameof(Advertisement));
            ConfigureNamingConventions(builder);
            Required(builder, nameof(Advertisement.Price), nameof(Advertisement.Estate) + "Id", nameof(Advertisement.User) + "Id");
        }
    }
}
