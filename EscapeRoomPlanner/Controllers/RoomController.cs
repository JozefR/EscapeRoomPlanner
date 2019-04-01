using System;
using System.Net.Http;
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
        public async Task<IActionResult> Index(bool seed = false)
        {
            if (seed)
            {
                _dataSeeder.SeedData();
            }

            var rooms = await _roomRepository.GetAllRoomsAsync();

            return View(rooms);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var room = await _roomRepository.GetRoomByIdAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        [HttpGet]
        [Route("/Room/Available/{roomId}/{selectedDate}")]
        public async Task<ActionResult> AvailableRoom(int roomId, string selectedDate)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri($"https://localhost:5001");

                HttpResponseMessage response = client.GetAsync($"/api/room/{roomId}/{selectedDate}").Result;

                response.EnsureSuccessStatusCode();

                return View("Shared/_roomAvailableHours", await response.Content.ReadAsAsync<DTO.Room>());
            }
        }
    }
}