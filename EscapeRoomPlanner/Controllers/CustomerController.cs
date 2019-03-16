using EscapeRoomPlanner.Data.EntityFramework.Repositories;
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

        public IActionResult Save()
        {
            var customer = _CustomerRepository.GetAllCustomersAsync();

            if (customer == null)
            {
                return NotFound();
            }

            return View();
        }
    }
}