﻿using CanWeFixItRepository.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanWeFixItRepository
{
    public class MarketDataRepository : IRepository<MarketDataDM>
    {
        // Using a name and a shared cache allows multiple connections to access the same
        // in-memory database
        const string connectionString = "Data Source=DatabaseService;Mode=Memory;Cache=Shared";
        private SqliteConnection _connection;

        public MarketDataRepository()
        {
            // The in-memory database only persists while a connection is open to it. To manage
            // its lifetime, keep one open connection around for as long as you need it.
            _connection = new SqliteConnection(connectionString);
            _connection.Open();
            const string createMarketData = @"
                CREATE TABLE marketdata
                (
                    id        int,
                    datavalue int,
                    sedol     text,
                    active    int
                );
                INSERT INTO marketdata
                VALUES (1, 1111, 'Sedol1', 0),
                       (2, 2222, 'Sedol2', 1),
                       (3, 3333, 'Sedol3', 0),
                       (4, 4444, 'Sedol4', 1),
                       (5, 5555, 'Sedol5', 0),
                       (6, 6666, 'Sedol6', 1)";

            _connection.Execute(createMarketData);
        }
        public async Task<IEnumerable<MarketDataDM>> GetAll()
        {
            return await _connection.QueryAsync<MarketDataDM>("SELECT * FROM MarketData");
        }

        public async Task<IEnumerable<MarketDataDM>> GetAllByFilter(string filter)
        {
            return await _connection.QueryAsync<MarketDataDM>(filter);
        }
    }
}
