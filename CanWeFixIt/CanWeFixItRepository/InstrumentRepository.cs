using CanWeFixItRepository.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanWeFixItRepository
{
    public class InstrumentRepository : IRepository<InstrumentDM>
    {
        // Using a name and a shared cache allows multiple connections to access the same
        // in-memory database
        const string connectionString = "Data Source=DatabaseService;Mode=Memory;Cache=Shared";
        private SqliteConnection _connection;

        public InstrumentRepository()
        {
            // The in-memory database only persists while a connection is open to it. To manage
            // its lifetime, keep one open connection around for as long as you need it.
            _connection = new SqliteConnection(connectionString);
            _connection.Open();
            const string createInstruments = @"
                CREATE TABLE instrument
                (
                    id     int,
                    sedol  text,
                    name   text,
                    active int
                );
                INSERT INTO instrument
                VALUES (1, 'Sedol1', 'Name1', 0),
                       (2, 'Sedol2', 'Name2', 1),
                       (3, 'Sedol3', 'Name3', 0),
                       (4, 'Sedol4', 'Name4', 1),
                       (5, 'Sedol5', 'Name5', 0),
                       (6, '', 'Name6', 1),
                       (7, 'Sedol7', 'Name7', 0),
                       (8, 'Sedol8', 'Name8', 1),
                       (9, 'Sedol9', 'Name9', 0)";

            _connection.Execute(createInstruments);
        }
        public async Task<IEnumerable<InstrumentDM>> GetAll()
        {
            return await _connection.QueryAsync<InstrumentDM>("SELECT * FROM instrument");
        }


        public Task<IEnumerable<InstrumentDM>> GetAllByFilter(string filter)
        {
            return _connection.QueryAsync<InstrumentDM>(filter);
        }
    }
}
