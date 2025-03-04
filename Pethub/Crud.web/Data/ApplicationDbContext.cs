using Crud.web.Models;
using Crud.web.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Crud.web.Data
{
    public class ApplicationDbContext : IdentityDbContext<Users>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Define your DbSets
        public DbSet<Pet> Pets { get; set; }

        // Configure model properties using Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customize Pet entity properties
            modelBuilder.Entity<Pet>(entity =>
            {
                entity.Property(e => e.ImagePath)
                      .IsRequired(false); // Allows NULL values for ImagePath
            });

            // You can add more customizations for Users or other entities if needed
        }
    }
}
