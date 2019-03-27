using System;
using System.Globalization;
using System.Security.Principal;
using System.Threading.Tasks;
using EscapeRoomPlanner.Data.EntityFramework.Models;
using EscapeRoomPlanner.Data.EntityFramework.Repositories;
using EscapeRoomPlanner.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EscapeRoomPlanner.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _CustomerRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly ICustomerReservationRepository _customerReservationRepository;

        public CustomerController(
            ICustomerRepository customerRepository,
            ICustomerReservationRepository customerReservationRepository,
            IRoomRepository roomRepository)
        {
            _CustomerRepository = customerRepository;
            _customerReservationRepository = customerReservationRepository;
            _roomRepository = roomRepository;
        }

        [Route("{roomId}/{selectedDate}/{selectedTime}")]
        [HttpGet]
        public async Task<IActionResult> New([FromRoute]int roomId, [FromRoute]string selectedDate, [FromRoute]int selectedTime)
        {
            var room = await _roomRepository.GetRoomByIdAsync(roomId);

            if (room == null)
            {
                return BadRequest();
            }

            if (!DateTime.TryParseExact(
                selectedDate,
                "dd.MM.yyyy",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out DateTime dateTime))
            {
                return BadRequest("Bad date format");
            };

            var newReservation = new NewReservationVM
            {
                RoomId = room.Id,
                RoomName = room.Name,
                SelectedDate = dateTime.ToString("dd.MM.yyyy"),
                SelectedOpenTime = selectedTime,
                SelectedCloseTime = selectedTime + 1,
            };

            return View("Save", newReservation);
        }

        [HttpPost]
        public async Task<ActionResult> Save(NewReservationVM newReservationVm)
        {
            DateTime.TryParseExact(
                newReservationVm.SelectedDate,
                "dd.MM.yyyy",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out DateTime dateTime);

            if (!ModelState.IsValid)
            {
                return View("Save", newReservationVm);
            }

            var room = await _roomRepository.GetRoomByIdAsync(newReservationVm.RoomId);

            var customer = new Customer
            {
                FirstName = newReservationVm.FirstName,
                SecondName = newReservationVm.SecondName,
                Email = newReservationVm.Email,
                PhoneNumber = newReservationVm.Email,
            };

            await _customerReservationRepository.UpdateCustomer(customer);

            var time = new TimeSpan(newReservationVm.SelectedOpenTime,0,0);
            var newDate = dateTime.Add(time);

            var reservation = new Reservation
            {
                Description = newReservationVm.Description,
                DateReservation = newDate,
                PhoneNumber = newReservationVm.PhoneNumber,
                CustomerId = customer.Id,
                RoomId = newReservationVm.RoomId
            };

            await _customerReservationRepository.UpdateReservation(reservation);
            room.Reservations.Add(reservation);

            await _customerReservationRepository.Save();

            return RedirectToAction(nameof(RoomController.Index), "Room");
        }
    }
}