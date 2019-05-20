using System;
using System.Linq;
using EscapeRoomPlanner.DTO;

namespace EscapeRoomPlanner.Infrastructure
{
    public static class ModelResponseToDTOMapper
    {
        public static Room MapRoomResponse(this Data.EntityFramework.Models.Room room, DateTime dateTime)
        {
            return new Room
            {
                Name = room.Name,
                Description = room.Description,
                OpeningTime = room.OpeningTime,
                ClosingTime = room.ClosingTime,
                Reservations = room.Reservations?.Select(r =>
                    new Reservation
                    {
                        DateReservation = r.DateReservation,
                        Description = r.Description,
                        PhoneNumber = r.PhoneNumber
                    }).ToList(),
                AvailableHours = room.AvailableHours(dateTime)
            };
        }
    }
}