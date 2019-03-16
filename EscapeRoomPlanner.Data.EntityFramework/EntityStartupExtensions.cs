using EscapeRoomPlanner.Data.EntityFramework.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EscapeRoomPlanner.Data.EntityFramework
{
    public static class EntityStartupExtensions
    {
        public static IServiceCollection ConfigureDbConnections(this IServiceCollection services) {

            var connection = @"Server=(localdb)\mssqllocaldb;Database=escapeRoomPlanner.db;Trusted_Connection=True;ConnectRetryCount=0";

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connection);
            });

            return services;
        }
    }
}