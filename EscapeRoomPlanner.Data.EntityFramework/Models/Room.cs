using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using EscapeRoomPlanner.DTO;

namespace EscapeRoomPlanner.Data.EntityFramework.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(0, 12)]
        public int OpeningTime { get; set; }

        [Required]
        [Range(12, 24)]
        public int ClosingTime { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

        public List<AvailableHour> AvailableHours(DateTime date)
        {
            var hourInterval = ClosingTime - OpeningTime;
            var availableHours = new List<AvailableHour>();

            for (int i = 0; i < hourInterval; i++)
            {
                var open = OpeningTime + i;
                var close = OpeningTime + i + 1;

                var alreadyReserved = Reservations
                    .Where(x => x.DateReservation.Date == date.Date)
                    .Select(x => x.DateReservation.Hour);

                if (!alreadyReserved.Contains(open))
                {
                    availableHours.Add(new AvailableHour(open, close));
                }
            }

            return availableHours;
        }
    }
}