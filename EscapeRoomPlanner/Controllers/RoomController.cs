using System;
using System.Net.Http;
using System.Threading.Tasks;
using EscapeRoomPlanner.Data.EntityFramework;
using EscapeRoomPlanner.Data.EntityFramework.Repositories;
using EscapeRoomPlanner.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EscapeRoomPlanner.Controllers
{
    public class RoomController : Controller
    {
        private readonly IDataSeeder _dataSeeder;
        private readonly IRoomRepository _roomRepository;

        public RoomController(
            IRoomRepository roomRepository,
            IDataSeeder dataSeeder)
        {
            _roomRepository = roomRepository;
            _dataSeeder = dataSeeder;
        }

        // GET
        public async Task<IActionResult> Index(bool seed = false)
        {
            if (seed) _dataSeeder.SeedData();

            var rooms = await _roomRepository.GetAllRoomsAsync();

            return View(rooms);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var room = await _roomRepository.GetRoomByIdAsync(id);

            if (room == null) return NotFound();

            return View(room);
        }

        [HttpGet]
        [Route(template: "/Room/Available/{roomId}/{selectedDate}")]
        public async Task<ActionResult> AvailableRoom(int roomId, string selectedDate)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001");

                var response = client.GetAsync($"/api/room/{roomId}/{selectedDate}").Result;

                response.EnsureSuccessStatusCode();

                return View(viewName: "Shared/_roomAvailableHours", model: await response.Content.ReadAsAsync<Room>());
            }
        }
    }
}