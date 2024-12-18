using Microsoft.EntityFrameworkCore;
using Storage.DataAccess.Configurations;
using Storage.Infrastructure.Configurations;
using Storage.Infrastructure.Entities;

namespace Storage.DataAccess
{
    public class StorageDbContext : DbContext
    {
        public StorageDbContext(DbContextOptions<StorageDbContext> options)
            : base(options) { }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<DepartmentEntity> Departments { get; set; }
        public DbSet<ManufacturerEntity> Manufacturers { get; set; }
        public DbSet<ModelEntity> Models { get; set; }
        public DbSet<PenaltyEntity> Penalties { get; set; }
        public DbSet<RentalEntity> Rentals { get; set; }
        public DbSet<ToolEntity> Tools { get; set; }
        public DbSet<WorkerEntity> Workers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new ManufacturerConfiguration());
            modelBuilder.ApplyConfiguration(new ModelConfiguration());
            modelBuilder.ApplyConfiguration(new PenaltyConfiguration());
            modelBuilder.ApplyConfiguration(new RentalConfiguration());
            modelBuilder.ApplyConfiguration(new ToolConfiguration());
            modelBuilder.ApplyConfiguration(new WorkerConfiguration());
        }
    }
}
