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

        public DbSet<ToolEntity> Tools { get; set; }
        public DbSet<WorkerEntity> Workers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ToolConfiguration());
            modelBuilder.ApplyConfiguration(new WorkerConfiguration());
            modelBuilder.ApplyConfiguration(new RentalConfiguration());
            modelBuilder.ApplyConfiguration(new OperationHistoryConfiguration());
        }
    }
}
