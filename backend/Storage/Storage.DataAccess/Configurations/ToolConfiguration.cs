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
    public class ToolConfiguration : IEntityTypeConfiguration<ToolEntity>
    {
        public void Configure(EntityTypeBuilder<ToolEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(b => b.Type)
                .HasMaxLength(Tool.MAX_STR_LENGTH)
                .IsRequired();

            builder.Property(b => b.Model)
                .IsRequired();

            builder.Property(b => b.Manufacturer)
                .IsRequired();

            builder.Property(b => b.Quantity)
                .IsRequired();

            builder.Property(b => b.IsTaken)
                .IsRequired();
        }
    }
}
