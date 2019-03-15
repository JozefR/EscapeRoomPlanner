using Microsoft.EntityFrameworkCore;

namespace EscapeRoomPlanner.Data.EntityFramework.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                :base(options)
        {

        }

        public DbSet<Room> Rooms { get; set; }
    }
}