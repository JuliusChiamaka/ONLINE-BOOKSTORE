
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineBookstore.Domain.Entities;
using System.Data;
using System.Data.SqlClient;

namespace OnlineBookstore.Infrastructure.Data
{
    public class DapperDbContext
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
            return new SqlConnection(_connectionString);
        }
    }
}
