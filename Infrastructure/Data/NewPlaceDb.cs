using ApplicationCore.Models;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Advertisement>();
            modelBuilder.Entity<Estate>();
            modelBuilder.Entity<User>();
        }
    }
}
