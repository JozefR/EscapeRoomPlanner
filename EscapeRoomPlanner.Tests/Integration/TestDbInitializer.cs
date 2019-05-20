using EscapeRoomPlanner.Data.EntityFramework;
using Microsoft.EntityFrameworkCore.Internal;

namespace EscapeRoomPlanner.Tests.Integration
{
    public class TestDbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Room.Any()) return;

            var roomData = DataSeeder.LoadData();

            context.Room.AddRange(roomData);
            context.SaveChanges();
        }
    }
}