/*
1. Aplikace dále vystaví REST API. Toto bude mít dvě metody. První metoda bude přijímat http GET požadavek
s parametrem - datum ve formátu dd.mm.yyyy. A bude vracet seznam všech místností a k nim informace o volných časech pro
rezervace na dané datum a o otevírací době jednotlivých místností.

2. Druhá bude analogická s tím rozdílem, že bude ty samé informace vracet ale pouze pro jednu místnost jejíž id bude předáno jako parametr.
Obě metody budou vracet JSON jehož strukturu navrhněte samostatně stejně tak jako podobu url, na kterém bude REST API dostupné.
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EscapeRoomPlanner.Data.EntityFramework.Repositories;
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
        public IActionResult Index()
        {
            return null;
        }

        /*
         * Analogicky, pre konkretnu miestnost. Na detaile budem mat konkretnu miestnost a casy ktore
         * su dostupne pre dany datum. Input bude id miestnosti a datum.
         */
        [Route("{roomId:int}")]
        [HttpGet]
        public async Task<ActionResult> Get([FromRoute]int roomId, [FromRoute]DateTime selectedDate)
        {
            // Ziskaj danu miestnost.
            var room = await _roomRepository.GetRoomByIdAsync(roomId);

            // Najdi rezervacie pre dany datum.

            var availableHours = room.AvailableHours(selectedDate);

            // Zobraz dostupne casy pre dany den. TODO: ako zobrazit interval casov?
            return Ok(availableHours);
        }

        /*
         * Pre dany datum potrebujem kontrolovat cas rezervacii vzhladom na otvaracie hodiny miestnosti.
         */
        private void workingRoomTime()
        {

        }

        /*
         * Taktiez ci uz na konkretnu hodinu neexistuje ina rezervacia.
         */
        private void reservationOverlap()
        {

        }

        /*
         * Od otvaracej hodiny potrebujem hodinovy interval dostupnych ,,casov,,.
         */

    }
}