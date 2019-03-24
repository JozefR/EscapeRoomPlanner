using System.Threading.Tasks;
using EscapeRoomPlanner.Data.EntityFramework.Models;

namespace EscapeRoomPlanner.Data.EntityFramework.Repositories
{
    public interface ICustomerReservationRepository
    {
        Task Save(Customer customer, Reservation reservation);
    }

    public class CustomerReservationRepository : ICustomerReservationRepository
    {
        private ApplicationDbContext _db;

        public CustomerReservationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Save(Customer customer, Reservation reservation)
        {


            await _db.SaveChangesAsync();
        }
    }
}