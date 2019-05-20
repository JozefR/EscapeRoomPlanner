using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using EscapeRoomPlanner.Data.EntityFramework.Repositories;
using EscapeRoomPlanner.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EscapeRoomPlanner.Controllers.API
{
    [Route(template: "api/[Controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;

        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        /*
         * Na zaklade datumu ako input parametru vrat vsetky rooms a dostupne rezervacie v dany datum
         * a konkretny cas.
         */
        [Route(template: "{selectedDate}")]
        public async Task<IActionResult> Get([FromRoute] string selectedDate)
        {
            var rooms = await _roomRepository.GetAllRoomsAsync();

            if (!DateTime.TryParseExact(
                selectedDate,
                format: "dd.MM.yyyy",
                provider: CultureInfo.InvariantCulture,
                style: DateTimeStyles.None,
                result: out var dateTime))
                return BadRequest(error: "Bad date format");

            var availableRooms = rooms.Select(r => r.MapRoomResponse(dateTime));

            return Ok(availableRooms);
        }

        /*
         * Analogicky, pre konkretnu miestnost. Na detaile budem mat konkretnu miestnost a casy ktore
         * su dostupne pre dany datum. Input bude id miestnosti a datum.
         */
        [Route(template: "{roomId}/{selectedDate}")]
        [HttpGet]
        public async Task<ActionResult> Get([FromRoute] int roomId, [FromRoute] string selectedDate)
        {
            if (!DateTime.TryParseExact(
                selectedDate,
                format: "dd.MM.yyyy",
                provider: CultureInfo.InvariantCulture,
                style: DateTimeStyles.None,
                result: out var dateTime))
                return BadRequest(error: "Bad date format");

            var room = await _roomRepository.GetRoomByIdAsync(roomId);

            if (room == null) return new NotFoundResult();

            var availableHours = room.MapRoomResponse(dateTime);

            return Ok(availableHours);
        }
    }
}