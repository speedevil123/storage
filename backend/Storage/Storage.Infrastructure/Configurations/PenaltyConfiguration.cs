using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Infrastructure.Configurations
{
    public class PenaltyConfiguration : IEntityTypeConfiguration<PenaltyEntity>
    {
        public void Configure(EntityTypeBuilder<PenaltyEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Fine)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(p => p.PenaltyDate)
                .IsRequired();

            builder.Property(p => p.IsPaidOut)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasOne(p => p.Rental)
                .WithMany(r => r.Penalties)
                .HasForeignKey(p => p.Id) // Ссылка на новый первичный ключ RentalEntity
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
