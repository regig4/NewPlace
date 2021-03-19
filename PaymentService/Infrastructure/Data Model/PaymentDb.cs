using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using PaymentService.ApplicationCore.Domain.Entities;

namespace Infrastructure
{
    public class PaymentDb : DbContext
    {
        public PaymentDb() { }

        public PaymentDb(DbContextOptions<PaymentDb> options) : base(options)
        {
        }

        public DbSet<Payment> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            #if DEBUG
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase("NewPlaceDbTest");
            #else
                .UseSqlServer(Infrastructure.Configuration.Configuration.DefaultConnectionString);
            #endif
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PaymentTypeConfiguration());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Payment>();
        }
    }
}
