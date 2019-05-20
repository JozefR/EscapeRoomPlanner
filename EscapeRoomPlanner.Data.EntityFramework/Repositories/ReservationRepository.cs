using System.Threading.Tasks;
using EscapeRoomPlanner.Data.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace EscapeRoomPlanner.Data.EntityFramework.Repositories
{
    public interface IReservationRepository
    {
        Task<Reservation> GetReservationByIdAsync(int id);
    }

    public class ReservationRepository : IReservationRepository
    {
        public ReservationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        private ApplicationDbContext _db { get; }

        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            var reservation = await _db.Reservation
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);

            return reservation;
        }
    }
}