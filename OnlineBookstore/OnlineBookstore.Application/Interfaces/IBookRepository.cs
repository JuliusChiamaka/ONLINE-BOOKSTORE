using OnlineBookstore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Service.Contract.Base
{
   public interface IBooksRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBooksByIdAsync(int id);
        Task AddBooksAsync(Book Books);
        Task UpdateBooksAsync(Book Books);
        Task DeleteBooksAsync(int id);
    }
}
