using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.DataModel
{
    class AdvertisementEntityTypeConfiguration : BaseEntityTypeConfiguration, IEntityTypeConfiguration<Advertisement>
    {
        public void Configure(EntityTypeBuilder<Advertisement> builder)
        {
            builder.ToTable(nameof(Advertisement));
            ConfigureNamingConventions(builder);
            Required(builder, nameof(Advertisement.Price), nameof(Advertisement.Estate) + "Id", nameof(Advertisement.User) + "Id");
        }
    }
}
