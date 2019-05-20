using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using EscapeRoomPlanner.Data.EntityFramework.Models;
using EscapeRoomPlanner.Data.EntityFramework.Repositories;
using EscapeRoomPlanner.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EscapeRoomPlanner.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ICustomerReservationRepository _customerReservationRepository;
        private readonly IRoomRepository _roomRepository;

        public ReservationController(
            ICustomerReservationRepository customerReservationRepository,
            IRoomRepository roomRepository)
        {
            _customerReservationRepository = customerReservationRepository;
            _roomRepository = roomRepository;
        }

        [Route(template: "{roomId}/{selectedDate}/{selectedTime}")]
        [HttpGet]
        public async Task<IActionResult> New([FromRoute] int roomId, [FromRoute] string selectedDate,
            [FromRoute] int selectedTime)
        {
            var room = await _roomRepository.GetRoomByIdAsync(roomId);

            if (room == null) return BadRequest();

            if (!DateTime.TryParseExact(
                selectedDate,
                format: "dd.MM.yyyy",
                provider: CultureInfo.CurrentCulture,
                style: DateTimeStyles.None,
                result: out var dateTime))
            {
                return BadRequest(error: "Bad date format");
            }

            var newReservation = new NewReservationVM
            {
                RoomId = room.Id,
                RoomName = room.Name,
                SelectedDate = dateTime.ToString(format: "dd.MM.yyyy"),
                SelectedOpenTime = selectedTime,
                SelectedCloseTime = selectedTime + 1
            };

            return View(viewName: "Save", model: newReservation);
        }

        [HttpPost]
        public async Task<ActionResult> Save(NewReservationVM newReservationVm)
        {
            DateTime.TryParseExact(
                newReservationVm.SelectedDate,
                format: "dd.MM.yyyy",
                provider: CultureInfo.CurrentCulture,
                style: DateTimeStyles.None,
                result: out var dateTime);

            if (!ModelState.IsValid) return View(viewName: "Save", model: newReservationVm);

            var room = await _roomRepository.GetRoomByIdAsync(newReservationVm.RoomId);

            if (!room.AvailableHours(dateTime).Select(x => x.Open).Contains(newReservationVm.SelectedOpenTime))
            {
                TempData[key: "ErrorMessage"] = "Sorry the room for chosen time was already reserved.";

                return RedirectToAction(nameof(RoomController.Detail), controllerName: "Room",
                    routeValues: new {id = newReservationVm.RoomId});
            }

            var customer = new Customer
            {
                FirstName = newReservationVm.FirstName,
                SecondName = newReservationVm.SecondName,
                Email = newReservationVm.Email,
                PhoneNumber = newReservationVm.Email
            };

            await _customerReservationRepository.AddCustomer(customer);

            var time = new TimeSpan(newReservationVm.SelectedOpenTime, 0, 0);
            var newDate = dateTime.Add(time);

            var reservation = new Reservation
            {
                Description = newReservationVm.Description,
                DateReservation = newDate,
                PhoneNumber = newReservationVm.PhoneNumber,
                CustomerId = customer.Id,
                RoomId = newReservationVm.RoomId
            };

            await _customerReservationRepository.AddReservation(reservation);

            room.Reservations.Add(reservation);

            await _customerReservationRepository.Save();

            return RedirectToAction(nameof(RoomController.Index), controllerName: "Room");
        }
    }
}