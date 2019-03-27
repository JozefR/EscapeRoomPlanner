using System.Threading.Tasks;
using EscapeRoomPlanner.Data.EntityFramework.Models;

namespace EscapeRoomPlanner.Data.EntityFramework.Repositories
{
    public interface ICustomerReservationRepository
    {
        Task UpdateCustomer(Customer customer);
        Task UpdateReservation(Reservation customer);
        Task Save();
    }

    public class CustomerReservationRepository : ICustomerReservationRepository
    {
        private ApplicationDbContext _db;

        public CustomerReservationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task UpdateCustomer(Customer customer)
        {
            await _db.AddAsync(customer);
        }

        public async Task UpdateReservation(Reservation customer)
        {
            await _db.AddAsync(customer);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}