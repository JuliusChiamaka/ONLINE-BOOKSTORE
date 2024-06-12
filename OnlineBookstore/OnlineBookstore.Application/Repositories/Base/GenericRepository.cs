using Dapper;
using OnlineBookstore.Application.Interfaces.Repository.Base;
using OnlineBookstore.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Application.Repositories.Base
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly IDapperContext _context;

        public GenericRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<T>($"SELECT * FROM {typeof(T).Name}s");
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<T>(
                    $"SELECT * FROM {typeof(T).Name}s WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task AddAsync(T entity)
        {
            var insertQuery = GenerateInsertQuery(entity);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(insertQuery, entity);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            var updateQuery = GenerateUpdateQuery(entity);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(updateQuery, entity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync($"DELETE FROM {typeof(T).Name}s WHERE Id = @Id", new { Id = id });
            }
        }

        private string GenerateInsertQuery(T entity)
        {
            var type = typeof(T);
            var tableName = type.Name + "s";
            var properties = type.GetProperties().Where(p => p.Name != "Id");
            var columnNames = string.Join(", ", properties.Select(p => p.Name));
            var parameterNames = string.Join(", ", properties.Select(p => "@" + p.Name));

            return $"INSERT INTO {tableName} ({columnNames}) VALUES ({parameterNames})";
        }

        private string GenerateUpdateQuery(T entity)
        {
            var type = typeof(T);
            var tableName = type.Name + "s";
            var properties = type.GetProperties().Where(p => p.Name != "Id");
            var setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));

            return $"UPDATE {tableName} SET {setClause} WHERE Id = @Id";
        }
    }

}
