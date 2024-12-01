﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Storage.DataAccess;

#nullable disable

namespace Storage.Infrastructure.Migrations
{
    [DbContext(typeof(StorageDbContext))]
    partial class StorageDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Storage.Infrastructure.Entities.OperationHistoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("OperationType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("ToolId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WorkerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ToolId");

                    b.HasIndex("WorkerId");

                    b.ToTable("OperationHistoryEntity");
                });

            modelBuilder.Entity("Storage.Infrastructure.Entities.RentalEntity", b =>
                {
                    b.Property<Guid>("WorkerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ToolId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("WorkerId", "ToolId");

                    b.HasIndex("ToolId");

                    b.ToTable("RentalEntity");
                });

            modelBuilder.Entity("Storage.Infrastructure.Entities.ToolEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsTaken")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Tools");
                });

            modelBuilder.Entity("Storage.Infrastructure.Entities.WorkerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("ToolEntityWorkerEntity", b =>
                {
                    b.Property<Guid>("ToolsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WorkersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ToolsId", "WorkersId");

                    b.HasIndex("WorkersId");

                    b.ToTable("ToolEntityWorkerEntity");
                });

            modelBuilder.Entity("Storage.Infrastructure.Entities.OperationHistoryEntity", b =>
                {
                    b.HasOne("Storage.Infrastructure.Entities.ToolEntity", "Tool")
                        .WithMany("OperationHistories")
                        .HasForeignKey("ToolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Storage.Infrastructure.Entities.WorkerEntity", "Worker")
                        .WithMany("OperationHistories")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tool");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("Storage.Infrastructure.Entities.RentalEntity", b =>
                {
                    b.HasOne("Storage.Infrastructure.Entities.ToolEntity", "Tool")
                        .WithMany("Rentals")
                        .HasForeignKey("ToolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Storage.Infrastructure.Entities.WorkerEntity", "Worker")
                        .WithMany("Rentals")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tool");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("ToolEntityWorkerEntity", b =>
                {
                    b.HasOne("Storage.Infrastructure.Entities.ToolEntity", null)
                        .WithMany()
                        .HasForeignKey("ToolsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Storage.Infrastructure.Entities.WorkerEntity", null)
                        .WithMany()
                        .HasForeignKey("WorkersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Storage.Infrastructure.Entities.ToolEntity", b =>
                {
                    b.Navigation("OperationHistories");

                    b.Navigation("Rentals");
                });

            modelBuilder.Entity("Storage.Infrastructure.Entities.WorkerEntity", b =>
                {
                    b.Navigation("OperationHistories");

                    b.Navigation("Rentals");
                });
#pragma warning restore 612, 618
        }
    }
}
