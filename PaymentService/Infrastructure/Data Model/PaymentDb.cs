using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
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

        public static readonly ILoggerFactory MyLoggerFactory
                = LoggerFactory.Create(builder => { builder.AddConsole(); });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
#if DEBUG
                .EnableSensitiveDataLogging()
                .UseLoggerFactory(MyLoggerFactory)
                //.UseInMemoryDatabase("NewPlaceDbTest");
                .UseSqlServer(Configuration.DefaultConnectionString);
#else
                .UseSqlServer(Configuration.DefaultConnectionString);
#endif
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("payment");

            modelBuilder.ApplyConfiguration(new PaymentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserTypeConfiguration());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Payment>();
        }
    }
}
