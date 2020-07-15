using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductApp.DAL.Entities;

namespace ProductApp.DAL.EF
{
    public class ApplicationContext : IdentityDbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Operation> Operations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.Name)
                       .IsClustered(false)
                       .IsUnique(true)
                       .HasFilter("[Name] IS NOT NULL")
                       .HasName("IX_Name");
            });
        }
    }
}
