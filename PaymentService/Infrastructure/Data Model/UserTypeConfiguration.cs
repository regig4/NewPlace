using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentService.ApplicationCore.Domain.Entities;

namespace Infrastructure.DataModel
{
    internal class UserTypeConfiguration : BaseEntityTypeConfiguration, IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigureNamingConventions(builder);
            builder.OwnsOne(p => p.PointsValue, sa =>
            {
                sa.Property(p => p.Amount).HasColumnName("points_amount");
            });
        }
    }
}
