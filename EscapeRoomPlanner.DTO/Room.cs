using System.Collections.Generic;

namespace EscapeRoomPlanner.DTO
{
    public class Room
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int OpeningTime { get; set; }

        public int ClosingTime { get; set; }

        public List<Reservation> Reservations { get; set; }

        public List<AvailableHour> AvailableHours { get; set; }
    }
}