using System.Collections.Generic;
using System.Threading.Tasks;
using EscapeRoomPlanner.Data.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace EscapeRoomPlanner.Data.EntityFramework.Repositories
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetAllRoomsAsync();
        Task<Room> GetRoomByIdAsync(int id);
    }

    public class RoomRepository : IRoomRepository
    {
        public RoomRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        private ApplicationDbContext _db { get; }

        public async Task<List<Room>> GetAllRoomsAsync()
        {
            var rooms = await _db.Room
                .ToListAsync();

            return rooms;
        }

        public async Task<Room> GetRoomByIdAsync(int id)
        {
            var room = await _db.Room
                .Include(x => x.Reservations)
                .SingleOrDefaultAsync(x => x.Id == id);

            return room;
        }
    }
}