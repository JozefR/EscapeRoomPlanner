using EscapeRoomPlanner.Data.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace EscapeRoomPlanner.Data.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                :base(options)
        {

        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}