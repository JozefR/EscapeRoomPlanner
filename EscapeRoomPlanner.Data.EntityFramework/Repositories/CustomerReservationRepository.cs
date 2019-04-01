using System.Threading.Tasks;
using EscapeRoomPlanner.Data.EntityFramework.Models;

namespace EscapeRoomPlanner.Data.EntityFramework.Repositories
{
    public interface ICustomerReservationRepository
    {
        Task AddCustomer(Customer customer);
        Task AddReservation(Reservation customer);
        Task Save();
    }

    public class CustomerReservationRepository : ICustomerReservationRepository
    {
        private ApplicationDbContext _db;

        public CustomerReservationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddCustomer(Customer customer)
        {
            await _db.AddAsync(customer);
        }

        public async Task AddReservation(Reservation customer)
        {
            await _db.AddAsync(customer);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}