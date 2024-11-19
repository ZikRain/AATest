using GRPC.DB.Configurations;
using Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace GRPC.DB.Contexts
{
    public class TestDbContext : DbContext
    {

        public DbSet<Worker> Workers { get; set; } = null!;
        public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TestDbEntitiesConfiguration());
        }
    }
}
