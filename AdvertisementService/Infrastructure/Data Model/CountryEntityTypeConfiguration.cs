using ApplicationCore.Models;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data_Model
{
    internal class CountryEntityTypeConfiguration : BaseEntityTypeConfiguration, IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            ConfigureNamingConventions(builder);
        }
    }
}
