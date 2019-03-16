using System.Threading.Tasks;
using EscapeRoomPlanner.Data.EntityFramework.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EscapeRoomPlanner.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Details(int id)
        {
            var reservation = await _reservationRepository.GetReservationByIdAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }
    }
}