using Dapper;
using OnlineBookstore.Application.Interfaces.Repository;
using OnlineBookstore.Application.Repositories.Base;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Application.Repositories
{
    public class CartRepository : GenericRepository<CartItem>, ICartRepository
    {
        private readonly IDapperContext _context;

        public CartRepository(IDapperContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(int userId)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "SELECT * FROM CARTITEM WHERE UserId = @UserId";
                return await connection.QueryAsync<CartItem>(query, new { UserId = userId });
            }
        }
    }
}
