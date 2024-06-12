using OnlineBookstore.Domain.Dtos.Request;
using OnlineBookstore.Domain.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Application.Interfaces.Service
{
    public interface IBookService
    {
        Task<IEnumerable<BookResponse>> GetAllBooksAsync();
        Task<BookResponse> GetBookByIdAsync(int id);
        Task AddBookAsync(AddBookRequest request);
        Task UpdateBookAsync(int id, UpdateBookRequest request);
        Task DeleteBookAsync(int id);
        Task<IEnumerable<BookResponse>> SearchBooksAsync(string title, string author, int? year, string genre);
    }
}
