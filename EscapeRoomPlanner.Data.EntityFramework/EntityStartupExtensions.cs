using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EscapeRoomPlanner.Data.EntityFramework
{
    public static class EntityStartupExtensions
    {
        public class DbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                // TODO: add configurations to appSettings.json
                var dbContextBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

                var connection = @"Server=(localdb)\mssqllocaldb;Database=escapeRoomPlanner.db;Trusted_Connection=True;ConnectRetryCount=0";

                dbContextBuilder.UseSqlServer(connection);

                return new ApplicationDbContext(dbContextBuilder.Options);
            }
        }
    }
}