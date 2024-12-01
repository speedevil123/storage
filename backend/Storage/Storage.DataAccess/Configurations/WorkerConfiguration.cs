using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Storage.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Infrastructure.Configurations
{
    public class WorkerConfiguration : IEntityTypeConfiguration<WorkerEntity>
    {
        public void Configure(EntityTypeBuilder<WorkerEntity> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(w => w.Position)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(w => w.Department)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(w => w.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(w => w.Phone)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(w => w.RegistrationDate)
                .IsRequired();

            builder.HasMany(w => w.Rentals)
                .WithOne(r => r.Worker)
                .HasForeignKey(r => r.WorkerId);

            builder.HasMany(w => w.OperationHistories)
                .WithOne(o => o.Worker)
                .HasForeignKey(o => o.WorkerId);
        }
    }

}
