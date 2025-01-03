﻿using Microsoft.EntityFrameworkCore;
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

            builder.Property(t => t.Quantity)
                .IsRequired();

            builder.HasOne(t => t.Manufacturer)
                .WithMany(m => m.Tools)
                .HasForeignKey(t => t.ManufacturerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Model)
                .WithMany(m => m.Tools)
                .HasForeignKey(m => m.ModelId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }

}
