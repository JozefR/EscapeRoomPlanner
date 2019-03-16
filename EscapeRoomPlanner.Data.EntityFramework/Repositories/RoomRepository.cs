using System.Collections.Generic;
using System.Threading.Tasks;
using EscapeRoomPlanner.Data.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace EscapeRoomPlanner.Data.EntityFramework.Repositories
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetAllRoomsAsync();
    }

    public class RoomRepository : IRoomRepository
    {
        private ApplicationDbContext _db { get; }

        public RoomRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Room>> GetAllRoomsAsync()
        {
            var rooms = await _db.Room
                .AsNoTracking()
                .ToListAsync();

            return rooms;
        }
    }
}