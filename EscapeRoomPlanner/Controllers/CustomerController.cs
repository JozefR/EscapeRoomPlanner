using EscapeRoomPlanner.Data.EntityFramework.Repositories;
using EscapeRoomPlanner.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EscapeRoomPlanner.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _CustomerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _CustomerRepository = customerRepository;
        }

        public IActionResult New()
        {
            return View("Save");
        }

        public IActionResult Save(NewReservationVM newReservationVm)
        {
            var test = newReservationVm;

            return View();
        }
    }
}