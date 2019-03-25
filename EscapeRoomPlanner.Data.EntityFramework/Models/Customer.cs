using System.Collections.Generic;

namespace EscapeRoomPlanner.Data.EntityFramework.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}