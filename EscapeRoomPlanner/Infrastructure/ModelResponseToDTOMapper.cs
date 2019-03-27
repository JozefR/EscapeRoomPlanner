using System;
using System.Linq;
using EscapeRoomPlanner.Data.EntityFramework.Models;

namespace EscapeRoomPlanner.Infrastructure
{
    public static class ModelResponseToDTOMapper
    {
        public static EscapeRoomPlanner.DTO.Room MapRoomResponse(this Room room, DateTime dateTime)
        {
            return new EscapeRoomPlanner.DTO.Room
            {
                Name = room.Name,
                Description = room.Description,
                OpeningTime = room.OpeningTime,
                ClosingTime = room.ClosingTime,
                Reservations = room.Reservations?.Select(r =>
                    new EscapeRoomPlanner.DTO.Reservation
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