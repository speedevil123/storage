using Microsoft.EntityFrameworkCore;
using Storage.Infrastructure.Entities;

namespace Storage.DataAccess
{
    public class StorageDbContext : DbContext
    {
        public StorageDbContext(DbContextOptions<StorageDbContext> options)
            : base(options) { }

        public DbSet<ToolEntity> Tools { get; set; }
    }
}
