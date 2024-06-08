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
    public class BookRepository : IBookRepository

    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public BookRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task AddBookAsync(Book book)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "INSERT INTO Books (Title, Genre, ISBN, Author, PublicationYear) VALUES (@Title, @Genre, @ISBN, @Author, @PublicationYear)";

                await connection.ExecuteAsync(sql, book);
            }
        }

        public async Task UpdateBookAsync(Book book)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "UPDATE Books SET Title = @Title, Genre = @Genre, ISBN = @ISBN, Author =@Author, PublicationYear = @PublicationYear WHERE Id = @Id";

                await connection.ExecuteAsync(sql, book);
              
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "DELETE FROM Books WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new {Id = id});
            }
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Book>("SELECT * FROM Books");
            }
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<Book>("SELECT * FROM Books WHERE Id = @Id", new { Id = id });
            }
        }

    }
}
