using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using OnlineBookstore.Application.Interfaces.Repository;
using OnlineBookstore.Application.Repositories.Base;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Service.Contract.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly IDapperContext _context;

        public BookRepository(IDapperContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> SearchAsync(string title, string author, int? year, string genre)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "SELECT * FROM BOOKS WHERE 1=1";
                if (!string.IsNullOrEmpty(title))
                    query += " AND Title LIKE @Title";
                if (!string.IsNullOrEmpty(author))
                    query += " AND Author LIKE @Author";
                if (year.HasValue)
                    query += " AND Year = @Year";
                if (!string.IsNullOrEmpty(genre))
                    query += " AND Genre LIKE @Genre";

                return await connection.QueryAsync<Book>(query, new { Title = $"%{title}%", Author = $"%{author}%", Year = year, Genre = $"%{genre}%" });
            }
        }
    }
}
