using System;
using EscapeRoomPlanner.Data.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace EscapeRoomPlanner.Data.EntityFramework
{
    // TODO: add configurations to appSettings.json
    public static class EntityStartupExtensions
    {
        public static IServiceCollection ConfigureDbConnections(this IServiceCollection services)
        {
            var connection = @"Server=(localdb)\mssqllocaldb;Database=escapeRoomPlanner.db;Trusted_Connection=True;ConnectRetryCount=0";

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connection);
            });

            services.AddTransient<IDataSeeder, DataSeeder>();
            services.AddTransient<IRoomRepository, RoomRepository>();

            return services;
        }

        // Only for migrations because of different assemblies.
        public class DbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                var dbContextBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

                var connection = @"Server=(localdb)\mssqllocaldb;Database=escapeRoomPlanner.db;Trusted_Connection=True;ConnectRetryCount=0";

                dbContextBuilder.UseSqlServer(connection);

                return new ApplicationDbContext(dbContextBuilder.Options);
            }
        }
    }
}