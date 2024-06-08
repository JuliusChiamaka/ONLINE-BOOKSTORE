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
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ShoppingCartRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public async Task AddToCartAsync(ShoppingCart cart)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "INSERT INTO ShoppingCart (UserId, BookId, Quantity) VALUES (@UserId, @BookId, @Quantity)";

                await connection.ExecuteAsync(sql, cart);
            }
        }

        public async Task<IEnumerable<ShoppingCart>> GetCartByUserIdAsync(int userId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QueryAsync<ShoppingCart>("SELECT * FROM ShoppingCart WHERE User", new { UserId = userId });
            }
        }

        public async Task RemoveFromCartAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "DELETE FROM ShoppingCarts WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}
