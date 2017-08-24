using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class NewPlaceDb : DbContext
    {
        DbSet<Advertisement> Advertisements { get; set; }
        DbSet<Apartment> Apartments { get; set; }
        DbSet<User> Users { get; set; }
    }
}
