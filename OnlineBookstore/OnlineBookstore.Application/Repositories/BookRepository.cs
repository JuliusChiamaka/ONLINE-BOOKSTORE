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

namespace OnlineBookstore.Service.Contract.Repository
{
    public class BooksRepository : IBooksRepository

    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public BooksRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task AddBooksAsync(Book Books)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "INSERT INTO Book (Title, Genre, ISBN, Author, PublicationYear) VALUES (@Title, @Genre, @ISBN, @Author, @PublicationYear)";

                await connection.ExecuteAsync(sql, Books);
            }
        }

        public async Task UpdateBooksAsync(Book Books)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "UPDATE Book SET Title = @Title, Genre = @Genre, ISBN = @ISBN, Author =@Author, PublicationYear = @PublicationYear WHERE Id = @Id";

                await connection.ExecuteAsync(sql, Books);
              
            }
        }

        public async Task DeleteBooksAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "DELETE FROM Book WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new {Id = id});
            }
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Book>("SELECT * FROM Book");
            }
        }

        public async Task<Book> GetBooksByIdAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<Book>("SELECT * FROM Book WHERE Id = @Id", new { Id = id });
            }
        }

    }
}
