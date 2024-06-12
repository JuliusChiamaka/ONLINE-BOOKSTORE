using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using OnlineBookstore.Application.Interfaces.Repository;
using OnlineBookstore.Application.Repositories.Base;
using OnlineBookstore.Domain.Dtos.Request;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Application.Repositories
{
    public class PurchaseHistoryRepository : GenericRepository<PurchaseHistory>, IPurchaseHistoryRepository
    {
        private readonly IDapperContext _context;

        public PurchaseHistoryRepository(IDapperContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PurchaseHistory>> GetPurchaseHistoryByUserIdAsync(int userId)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "SELECT * FROM PURCHASEHISTORYS WHERE APP_USER_ID = @UserId";
                return await connection.QueryAsync<PurchaseHistory>(query, new { UserId = userId });
            }
        } 

        public async Task<PurchaseHistoryDto> GetPurchaseHistoryWithItemsAsync(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = @"
                SELECT * FROM PURCHASEHISTORYS ph
                LEFT JOIN PURCHASEHISTORYITEMS phi ON ph.Id = phi.PURCHASE_HISTORY_ID
                LEFT JOIN BOOKS b ON phi.BOOK_ID = b.Id
                WHERE ph.Id = @Id";

                var purchaseHistoryDictionary = new Dictionary<int, PurchaseHistoryDto>();

                var result = await connection.QueryAsync<PurchaseHistoryDto, PurchaseHistoryItemDto, BookDto, PurchaseHistoryDto>(
                    sql,
                    (ph, phi, b) =>
                    {
                        if (!purchaseHistoryDictionary.TryGetValue(ph.Id, out var currentPurchaseHistory))
                        {
                            currentPurchaseHistory = ph;
                            purchaseHistoryDictionary.Add(currentPurchaseHistory.Id, currentPurchaseHistory);
                        }

                        if (phi != null)
                        {
                            if (b != null)
                            {
                                phi.Book = b;
                            }
                            currentPurchaseHistory.Items.Add(phi);
                        }

                        return currentPurchaseHistory;
                    },
                    new { Id = id }
                );

                return result.FirstOrDefault();
            }
        }
    }
}
