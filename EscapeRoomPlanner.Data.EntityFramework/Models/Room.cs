using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EscapeRoomPlanner.Data.EntityFramework.Models
{
    public class Room
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int OpeningTime { get; set; }

        public int ClosingTime { get; set; }

        public List<Reservation> Reservations { get; set; } = new List<Reservation>();

        public List<AvailableHour> AvailableHours(DateTime date)
        {
            var hourInterval = ClosingTime - OpeningTime;
            var availableHours = new List<AvailableHour>();

            for (int i = 0; i < hourInterval; i++)
            {
                var open = OpeningTime + i;
                var close = OpeningTime + i + 1;

                var alreadyReserved = Reservations
                    .Where(x => x.DateReservation == date)
                    .Select(x => x.DateReservation.Date.Hour);

                if (!alreadyReserved.Contains(open))
                {
                    availableHours.Add(new AvailableHour(open, close));
                }
            }

            return availableHours;
        }
    }

    [NotMapped]
    public class AvailableHour
    {
        public int Open;
        public int Close;

        public AvailableHour(int open, int close)
        {
            Open = open;
            Close = close;
        }
    }
}