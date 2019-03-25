using EscapeRoomPlanner.Data.EntityFramework.Models;
using EscapeRoomPlanner.Data.EntityFramework.Repositories;
using EscapeRoomPlanner.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EscapeRoomPlanner.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _CustomerRepository;
        private readonly ICustomerReservationRepository _customerReservationRepository;

        public CustomerController(
            ICustomerRepository customerRepository,
            ICustomerReservationRepository customerReservationRepository)
        {
            _CustomerRepository = customerRepository;
            _customerReservationRepository = customerReservationRepository;
        }

        [HttpPost]
        public IActionResult New([FromBody]Room room)
        {
            var newReservation = new NewReservationVM
            {
                Room = room,
            };

            return View("Save", newReservation);
        }

        [HttpPost]
        public ActionResult Save(NewReservationVM newReservationVm)
        {
            var customer = new Customer
            {
                FirstName = newReservationVm.FirstName,
                SecondName = newReservationVm.SecondName,
                Email = newReservationVm.Email,
                PhoneNumber = newReservationVm.Email,
            };

            var reservation = new Reservation
            {
                Description = newReservationVm.Description,
                Customer = customer
            };

            _customerReservationRepository.Save(customer, reservation);

            return View();
        }
    }
}