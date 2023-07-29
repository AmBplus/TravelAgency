using AirlineReservationSystem.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservationSystem.Test
{
    public class InMemoryDbContext

    {
        private readonly SqliteConnection connection;
        private readonly DbContextOptions<TravelAgencyDbContext> dbContextOptions;

        public InMemoryDbContext()
        {
            connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            dbContextOptions = new DbContextOptionsBuilder<TravelAgencyDbContext>()
                .UseSqlite(connection)
                .Options;

            using var context = new TravelAgencyDbContext(dbContextOptions);

            context.Database.EnsureCreated();
        }

        public TravelAgencyDbContext CreateContext() => new TravelAgencyDbContext(dbContextOptions);

        public void Dispose() => connection.Dispose();

    }
}
