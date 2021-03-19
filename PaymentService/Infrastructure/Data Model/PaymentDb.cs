using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using PaymentService.ApplicationCore.Domain.Entities;
using PaymentService.Infrastructure.Configuration;

namespace Infrastructure
{
    public class PaymentDb : DbContext
    {
        public PaymentDb() { }

        public PaymentDb(DbContextOptions<PaymentDb> options) : base(options)
        {
        }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
#if DEBUG
                .EnableSensitiveDataLogging()
                //.UseInMemoryDatabase("NewPlaceDbTest");
                .UseSqlServer(Configuration.DefaultConnectionString);
#else
                .UseSqlServer(Infrastructure.Configuration.Configuration.DefaultConnectionString);
#endif
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("payment");

            modelBuilder.ApplyConfiguration(new PaymentTypeConfiguration());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Payment>();
        }
    }
}
