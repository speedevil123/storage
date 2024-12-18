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
                .IsRequired();

            builder.Property(w => w.Position)
                .IsRequired();

            builder.Property(w => w.Email)
                .IsRequired();

            builder.Property(w => w.PhoneNumber)
                .IsRequired();

            builder.Property(w => w.RegistrationDate)
                .IsRequired();

            builder.HasOne(w => w.Department)
                .WithOne(d => d.Worker)
                .HasForeignKey<WorkerEntity>(w => w.DepartmentId);
        }
    }

}
