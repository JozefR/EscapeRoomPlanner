using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using EscapeRoomPlanner.Data.EntityFramework.Repositories;
using EscapeRoomPlanner.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EscapeRoomPlanner.Controllers.API
{
    [Route("api/[Controller]")]
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
         [Route("{selectedDate}")]
        public async Task<IActionResult> Get([FromRoute]string selectedDate)
        {
            var rooms = await _roomRepository.GetAllRoomsAsync();

            if (!DateTime.TryParseExact(
                selectedDate,
                "dd.MM.yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime dateTime))
            {
                return BadRequest("Bad date format");
            };

            var availableRooms = rooms.Select(r => r.MapRoomResponse(dateTime));

            return Ok(availableRooms);
        }

        /*
         * Analogicky, pre konkretnu miestnost. Na detaile budem mat konkretnu miestnost a casy ktore
         * su dostupne pre dany datum. Input bude id miestnosti a datum.
         */
        [Route("{roomId}/{selectedDate}")]
        [HttpGet]
        public async Task<ActionResult> Get([FromRoute]int roomId, [FromRoute]string selectedDate)
        {
            var room = await _roomRepository.GetRoomByIdAsync(roomId);

            if (!DateTime.TryParseExact(
                selectedDate,
                "dd.MM.yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime dateTime))
            {
                return BadRequest("Bad date format");
            };

            var availableHours = room.MapRoomResponse(dateTime);

            return Ok(availableHours);
        }
    }
}