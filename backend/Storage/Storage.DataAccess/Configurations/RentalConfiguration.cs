using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Storage.Core.Models;
using Storage.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Infrastructure.Configurations
{
    public class RentalConfiguration : IEntityTypeConfiguration<RentalEntity>
    {
        public void Configure(EntityTypeBuilder<RentalEntity> builder)
        {
            builder.HasKey(r => new { r.WorkerId, r.ToolId }); // Composite key if needed

            builder.Property(r => r.StartDate)
                .IsRequired();

            builder.Property(r => r.ReturnDate)
                .IsRequired(); // Nullable

            builder.Property(r => r.Status)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(r => r.Worker)
                .WithMany(w => w.Rentals)
                .HasForeignKey(r => r.WorkerId);

            builder.HasOne(r => r.Tool)
                .WithMany(t => t.Rentals)
                .HasForeignKey(r => r.ToolId);
        }
    }
}
