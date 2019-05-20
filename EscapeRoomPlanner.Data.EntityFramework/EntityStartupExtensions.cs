using EscapeRoomPlanner.Data.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EscapeRoomPlanner.Data.EntityFramework
{
    // TODO: add configurations to appSettings.json
    public static class EntityStartupExtensions
    {
        public static IServiceCollection ConfigureDbConnections(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(DbSettings.LocalMsSqlConnection);
            });

            services.AddTransient<IDataSeeder, DataSeeder>();
            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICustomerReservationRepository, CustomerReservationRepository>();

            return services;
        }

        // Only for migrations because of different assemblies.
        public class DbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                var dbContextBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

                dbContextBuilder.UseSqlServer(DbSettings.LocalMsSqlConnection);

                return new ApplicationDbContext(dbContextBuilder.Options);
            }
        }
    }
}