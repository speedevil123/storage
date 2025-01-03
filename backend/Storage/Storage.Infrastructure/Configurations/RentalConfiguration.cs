// Configuration
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Storage.Infrastructure.Entities;

namespace Storage.Infrastructure.Configurations
{
    public class RentalConfiguration : IEntityTypeConfiguration<RentalEntity>
    {
        public void Configure(EntityTypeBuilder<RentalEntity> builder)
        {
            builder.HasKey(r => r.Id); // Новый первичный ключ

            builder.Property(r => r.Id)
                .IsRequired();

            builder.Property(r => r.StartDate)
                .IsRequired();

            builder.Property(r => r.ReturnDate)
                .IsRequired();

            builder.Property(r => r.EndDate)
                .IsRequired();

            builder.Property(r => r.Status)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(r => r.ToolQuantity)
                .IsRequired();

            builder.HasOne(r => r.Worker)
                .WithMany(w => w.Rentals)
                .HasForeignKey(r => r.WorkerId);

            builder.HasOne(r => r.Tool)
                .WithMany(t => t.Rentals)
                .HasForeignKey(r => r.ToolId);

            builder.HasIndex(r => new { r.WorkerId, r.ToolId }) // Индекс для составного ключа
                .IsUnique(false);
        }
    }
}
