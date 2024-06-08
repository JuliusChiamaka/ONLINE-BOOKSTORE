using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Service.Contract.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Application.Repositories
{
    public class PurchaseHistoryRepository : IPurchaseHistoryRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public PurchaseHistoryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public async Task AddPurchaseHistoryAsync(PurchaseHistory history)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "INSERT INTO PurchaseHistory (UserId, BookId, PurchaseDate) VALUES (@UserId, @BookId, @PurchaseDate)";

                await connection.ExecuteAsync(sql, history);
            }
        }

        public async Task<IEnumerable<PurchaseHistory>> GetPurchaseHistoryByUserIdAsync(int userId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QueryAsync<PurchaseHistory>("SELECT * FROM PurchaseHistory WHERE UserId = @UserId", new { UserId = userId });
            }
        }
    }
}
