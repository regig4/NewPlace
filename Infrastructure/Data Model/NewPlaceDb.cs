using ApplicationCore.Models;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class NewPlaceDb : DbContext
    {
        public NewPlaceDb(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Estate> Apartments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            #if DEBUG
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase("NewPlaceDbTest");
                //.UseSqlServer(Infrastructure.Configuration.Configuration.DefaultConnectionString);
            #else
                .UseSqlServer(Infrastructure.Configuration.Configuration.DefaultConnectionString);
            #endif
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AdvertisementEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UtilityEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EstateEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LocationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AgencyEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Advertisement>();
            modelBuilder.Entity<Estate>();
            modelBuilder.Entity<User>();
        }
    }
}
