using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentService.Infrastructure.Utils;

namespace Infrastructure.DataModel
{
    internal class BaseEntityTypeConfiguration
    {
        public void ConfigureNamingConventions<T>(EntityTypeBuilder<T> builder) where T : class
        {
            IEnumerable<Microsoft.EntityFrameworkCore.Metadata.IMutableProperty> properties = builder.Metadata.GetProperties();

            foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableProperty prop in properties)
            {
                builder.Property(prop.Name).HasColumnName(Utils.CreateColumnName(prop.Name));
            }

            IEnumerable<Microsoft.EntityFrameworkCore.Metadata.IMutableNavigation> navs = builder.Metadata.GetNavigations();

            foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableNavigation nav in navs)
            {
                if (builder.Metadata.FindProperty(nav.Name + "Id") != null)
                {
                    builder.Property(nav.Name + "Id").HasColumnName(Utils.CreateColumnName(nav.Name + "Id"));
                }
            }
        }

        public void Required<T>(EntityTypeBuilder<T> builder, params string[] propNames) where T : class
        {
            foreach (string propName in propNames)
            {
                builder.Property(propName).IsRequired();
            }
        }
    }
}
