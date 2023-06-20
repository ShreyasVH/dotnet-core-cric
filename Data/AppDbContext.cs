using System;
using Microsoft.EntityFrameworkCore;

using Com.Dotnet.Cric.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
        public DbSet<TeamType> TeamTypes { get; set; }
        public DbSet<Team> Teams { get; set;  }

        public DbSet<Tour> Tours { get; set; }
        
        public DbSet<Player> Players { get; set; }

        public DbSet<Series> Series { get; set; }
        public DbSet<SeriesType> SeriesTypes { get; set; }
        public DbSet<GameType> GameTypes { get; set; }
        public DbSet<SeriesTeamsMap> SeriesTeamsMap { get; set; }

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
                .HasDatabaseName("country");

            modelBuilder.Entity<TeamType>()
                .HasIndex(tt => new { tt.Name })
                .IsUnique();

            modelBuilder.Entity<Team>()
                .HasOne(t => t.Country)
                .WithMany()
                .HasForeignKey(t => t.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Team>()
                .HasOne(t => t.Type)
                .WithMany()
                .HasForeignKey(t => t.TypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Team>()
                .HasIndex(t => new { t.Name, t.CountryId, t.TypeId })
                .IsUnique();

            modelBuilder.Entity<Team>()
                .HasIndex(s => s.CountryId)
                .HasDatabaseName("country");

            modelBuilder.Entity<Tour>()
               .HasIndex(t => new { t.Name, t.StartTime })
               .IsUnique();
            
            var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
                v => new DateTime(v.Year, v.Month, v.Day),
                v => new DateOnly(v.Year, v.Month, v.Day));

            modelBuilder.Entity<Player>()
                .Property(p => p.DateOfBirth)
                .HasConversion(dateOnlyConverter);
            
            modelBuilder.Entity<Player>()
                .HasOne(p => p.Country)
                .WithMany()
                .HasForeignKey(p => p.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Player>()
                .HasIndex(p => new { p.Name, p.CountryId, p.DateOfBirth })
                .IsUnique();

            modelBuilder.Entity<Player>()
                .HasIndex(p => p.CountryId)
                .HasDatabaseName("country");
            
            modelBuilder.Entity<SeriesType>()
                .HasIndex(st => new { st.Name })
                .IsUnique();
            
            modelBuilder.Entity<GameType>()
                .HasIndex(gt => new { gt.Name })
                .IsUnique();

            modelBuilder.Entity<Series>()
                .HasOne(s => s.HomeCountry)
                .WithMany()
                .HasForeignKey(s => s.HomeCountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Series>()
                .HasOne(s => s.Tour)
                .WithMany()
                .HasForeignKey(s => s.TourId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Series>()
                .HasOne(s => s.Type)
                .WithMany()
                .HasForeignKey(s => s.TypeId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Series>()
                .HasOne(s => s.GameType)
                .WithMany()
                .HasForeignKey(s => s.GameTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Series>()
                .HasIndex(t => new { t.Name, t.TourId, t.GameTypeId })
                .IsUnique();

            modelBuilder.Entity<Series>()
                .HasIndex(s => s.HomeCountryId)
                .HasDatabaseName("HomeCountry");
            
            modelBuilder.Entity<Series>()
                .HasIndex(s => s.TourId)
                .HasDatabaseName("Tour");
            
            modelBuilder.Entity<Series>()
                .HasIndex(s => s.TypeId)
                .HasDatabaseName("Type");
            
            modelBuilder.Entity<Series>()
                .HasIndex(s => s.GameTypeId)
                .HasDatabaseName("GameType");
            
            modelBuilder.Entity<SeriesTeamsMap>()
                .HasOne(s => s.Series)
                .WithMany()
                .HasForeignKey(s => s.SeriesId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SeriesTeamsMap>()
                .HasOne(s => s.Team)
                .WithMany()
                .HasForeignKey(s => s.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SeriesTeamsMap>()
                .HasIndex(s => new { s.SeriesId, s.TeamId })
                .IsUnique();

            modelBuilder.Entity<SeriesTeamsMap>()
                .HasIndex(s => s.SeriesId)
                .HasDatabaseName("Series");
            
            modelBuilder.Entity<SeriesTeamsMap>()
                .HasIndex(s => s.TeamId)
                .HasDatabaseName("Team");

            base.OnModelCreating(modelBuilder);
        }
    }
}
