using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentService.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DataModel
{
    class BaseEntityTypeConfiguration
    {
        public void ConfigureNamingConventions<T>(EntityTypeBuilder<T> builder) where T : class
        {
            var properties = builder.Metadata.GetProperties();

            foreach (var prop in properties)
                builder.Property(prop.Name).HasColumnName(Utils.CreateColumnName(prop.Name));

            var navs = builder.Metadata.GetNavigations();

            foreach (var nav in navs)
                if (builder.Metadata.FindProperty(nav.Name + "Id") != null)
                    builder.Property(nav.Name + "Id").HasColumnName(Utils.CreateColumnName(nav.Name + "Id"));

        }

        public void Required<T>(EntityTypeBuilder<T> builder, params string[] propNames) where T : class
        {
            foreach (var propName in propNames)
                builder.Property(propName).IsRequired();
        }
    }
}
