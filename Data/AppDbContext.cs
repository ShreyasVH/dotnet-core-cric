using Microsoft.EntityFrameworkCore;

using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Define your entities (models) as DbSet properties
        public DbSet<Country> Countries { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure any additional model-related settings or relationships here
            // For example:
            // modelBuilder.Entity<User>()
            //     .HasMany(u => u.Products)
            //     .WithOne(p => p.User)
            //     .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Stadium>()
                .HasOne(s => s.Country)
                .WithMany()
                .HasForeignKey(s => s.CountryId)
                .OnDelete(DeleteBehavior.Restrict); // Specify the onDelete property

            modelBuilder.Entity<Stadium>()
                .HasIndex(s => new { s.Name, s.CountryId })
                .IsUnique();

            modelBuilder.Entity<Stadium>()
                .HasIndex(s => s.CountryId)
                .HasName("country");

            base.OnModelCreating(modelBuilder);
        }
    }
}
