using Microsoft.EntityFrameworkCore;
using ProductViewer.Database.Entities;
using ProductViewer.EntityFramework.Configurations;

namespace ProductViewer.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ProductEntity> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}