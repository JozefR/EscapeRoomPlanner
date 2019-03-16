using System.Threading.Tasks;
using EscapeRoomPlanner.Data.EntityFramework.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EscapeRoomPlanner.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomRepository _roomRepository;

        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            var rooms = await _roomRepository.GetAllRoomsAsync();

            return View();
        }
    }
}