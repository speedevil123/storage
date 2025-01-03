﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Storage.DataAccess;

#nullable disable

namespace Storage.Infrastructure.Migrations
{
    [DbContext(typeof(StorageDbContext))]
    [Migration("20250103060619_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Storage.Infrastructure.Entities.CategoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Storage.Infrastructure.Entities.DepartmentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Storage.Infrastructure.Entities.ManufacturerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostIndex")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("Storage.Infrastructure.Entities.ModelEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("Storage.Infrastructure.Entities.PenaltyEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Fine")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValue(0.0);

                    b.Property<bool>("IsPaidOut")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("PenaltyDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RentalId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RentalId");

                    b.ToTable("Penalties");
                });

            modelBuilder.Entity("Storage.Infrastructure.Entities.RentalEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<Guid>("ToolId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ToolQuantity")
                        .HasColumnType("int");

                    b.Property<Guid>("WorkerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ToolId");

                    b.HasIndex("WorkerId", "ToolId");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("Storage.Infrastructure.Entities.ToolEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ManufacturerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ModelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.HasIndex("ModelId");

                    b.ToTable("Tools");
                });

            modelBuilder.Entity("Storage.Infrastructure.Entities.WorkerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("Storage.Infrastructure.Entities.ModelEntity", b =>
                {
                    b.HasOne("Storage.Infrastructure.Entities.CategoryEntity", "Category")
                        .WithMany("Models")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Storage.Infrastructure.Entities.PenaltyEntity", b =>
                {
                    b.HasOne("Storage.Infrastructure.Entities.RentalEntity", "Rental")
                        .WithMany("Penalties")
                        .HasForeignKey("RentalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rental");
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

            modelBuilder.Entity("Storage.Infrastructure.Entities.ToolEntity", b =>
                {
                    b.HasOne("Storage.Infrastructure.Entities.ManufacturerEntity", "Manufacturer")
                        .WithMany("Tools")
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Storage.Infrastructure.Entities.ModelEntity", "Model")
                        .WithMany("Tools")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacturer");

                    b.Navigation("Model");
                });

            modelBuilder.Entity("Storage.Infrastructure.Entities.WorkerEntity", b =>
                {
                    b.HasOne("Storage.Infrastructure.Entities.DepartmentEntity", "Department")
                        .WithMany("Workers")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Storage.Infrastructure.Entities.CategoryEntity", b =>
                {
                    b.Navigation("Models");
                });

            modelBuilder.Entity("Storage.Infrastructure.Entities.DepartmentEntity", b =>
                {
                    b.Navigation("Workers");
                });

            modelBuilder.Entity("Storage.Infrastructure.Entities.ManufacturerEntity", b =>
                {
                    b.Navigation("Tools");
                });

            modelBuilder.Entity("Storage.Infrastructure.Entities.ModelEntity", b =>
                {
                    b.Navigation("Tools");
                });

            modelBuilder.Entity("Storage.Infrastructure.Entities.RentalEntity", b =>
                {
                    b.Navigation("Penalties");
                });

            modelBuilder.Entity("Storage.Infrastructure.Entities.ToolEntity", b =>
                {
                    b.Navigation("Rentals");
                });

            modelBuilder.Entity("Storage.Infrastructure.Entities.WorkerEntity", b =>
                {
                    b.Navigation("Rentals");
                });
#pragma warning restore 612, 618
        }
    }
}
