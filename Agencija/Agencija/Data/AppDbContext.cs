using Agencija.Models;
using Microsoft.EntityFrameworkCore;

namespace Agencija.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options) { }


        public DbSet<User> Users { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<TourPackage> TourPackages { get; set; }


        public DbSet<ReservationEvent> ReservationEvents { get; set; }





    }
}
