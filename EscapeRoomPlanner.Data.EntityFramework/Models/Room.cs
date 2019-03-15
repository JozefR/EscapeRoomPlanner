using System;

namespace EscapeRoomPlanner.Data.EntityFramework.Models
{
    public class Room
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int OpeningTime { get; set; }

        public int ClosingTime { get; set; }
    }
}