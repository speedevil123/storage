using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Core.Models;
using Storage.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DataAccess.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ToolConfiguration : IEntityTypeConfiguration<ToolEntity>
    {
        public void Configure(EntityTypeBuilder<ToolEntity> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Model)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Manufacturer)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Quantity)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(t => t.IsTaken)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }

}
