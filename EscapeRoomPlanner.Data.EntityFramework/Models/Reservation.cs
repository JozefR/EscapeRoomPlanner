using System;

namespace EscapeRoomPlanner.Data.EntityFramework.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public DateTime DateReservation { get; set; }

        public string PhoneNumber { get; set; }

        public string Description { get; set; }

        public int RoomId { get; set; }

        public int CustomerId { get; set; }
    }
}