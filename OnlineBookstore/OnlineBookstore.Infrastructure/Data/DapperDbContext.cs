
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using OnlineBookstore.Domain.Entities;
using System.Data;
using System.Data.SqlClient;

namespace OnlineBookstore.Infrastructure.Data
{
    public class DapperDbContext : IDapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
