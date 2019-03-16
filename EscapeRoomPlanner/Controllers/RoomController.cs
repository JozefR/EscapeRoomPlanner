using System.Threading.Tasks;
using EscapeRoomPlanner.Data.EntityFramework;
using EscapeRoomPlanner.Data.EntityFramework.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EscapeRoomPlanner.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IDataSeeder _dataSeeder;

        public RoomController(
            IRoomRepository roomRepository,
            IDataSeeder dataSeeder)
        {
            _roomRepository = roomRepository;
            _dataSeeder = dataSeeder;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            _dataSeeder.SeedData();

            var rooms = await _roomRepository.GetAllRoomsAsync();

            return View(rooms);
        }

        public async Task<IActionResult> Details(int id)
        {
            var room = await _roomRepository.GetRoomByIdAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }
    }
}