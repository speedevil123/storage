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
            builder.HasKey(x => x.Id);

        }
    }
     
    //public Guid Id { get; }
    //public DateTime StartDate { get; } = new DateTime();
    //public DateTime? ReturnDate { get; } = new DateTime();
    //public string Status { get; } = string.Empty; // Статус (активен, завершен, просрочен)

    ////Navigation Properties-ForeignKeys
    //public Guid WorkerId { get; }
    //public Guid ToolId { get; }

    //public Worker Worker { get; }
    //public Tool Tool { get; }
    
    //////////////////////

    //public class ToolConfiguration : IEntityTypeConfiguration<ToolEntity>
    //{
    //    public void Configure(EntityTypeBuilder<ToolEntity> builder)
    //    {
    //        builder.HasKey(x => x.Id);

    //        builder.Property(b => b.Type)
    //            .HasMaxLength(Tool.MAX_STR_LENGTH)
    //            .IsRequired();

    //        builder.Property(b => b.Model)
    //            .IsRequired();

    //        builder.Property(b => b.Manufacturer)
    //            .IsRequired();

    //        builder.Property(b => b.Quantity)
    //            .IsRequired();

    //        builder.Property(b => b.IsTaken)
    //            .IsRequired();
    //    }
    //}
}
