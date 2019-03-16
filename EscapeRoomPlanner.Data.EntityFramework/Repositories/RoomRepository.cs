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

        public async Task<Room> GetRoomByIdAsync(int id)
        {
            var room = await _db.Room
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);

            return room;
        }
    }
}