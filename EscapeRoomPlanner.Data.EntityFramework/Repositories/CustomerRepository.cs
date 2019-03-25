using System.Collections.Generic;
using System.Threading.Tasks;
using EscapeRoomPlanner.Data.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace EscapeRoomPlanner.Data.EntityFramework.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private ApplicationDbContext _db { get; }

        public CustomerRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            var customers = await _db.Customer
                .AsNoTracking()
                .ToListAsync();

            return customers;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var customer = await _db.Customer
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);

            return customer;
        }
    }
}