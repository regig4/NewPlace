using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataModel
{
    internal class UserEntityTypeConfiguration : BaseEntityTypeConfiguration, IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            ConfigureNamingConventions(builder);
            Required(builder, nameof(User.Login), nameof(User.PasswordHash));
            builder.Property(u => u.PasswordHash).HasMaxLength(512);
        }
    }
}
