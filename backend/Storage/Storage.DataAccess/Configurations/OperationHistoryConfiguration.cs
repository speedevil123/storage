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
    public class OperationHistoryConfiguration : IEntityTypeConfiguration<OperationHistoryEntity>
    {
        public void Configure(EntityTypeBuilder<OperationHistoryEntity> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.OperationType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(o => o.Date)
                .IsRequired();

            builder.Property(o => o.Comment)
                .IsRequired(false) // Nullable
                .HasMaxLength(250);

            builder.HasOne(o => o.Worker)
                .WithMany(w => w.OperationHistories)
                .HasForeignKey(o => o.WorkerId);

            builder.HasOne(o => o.Tool)
                .WithMany(t => t.OperationHistories)
                .HasForeignKey(o => o.ToolId);
        }
    }

}
