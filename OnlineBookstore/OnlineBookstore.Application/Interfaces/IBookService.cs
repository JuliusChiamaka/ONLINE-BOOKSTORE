using OnlineBookstore.Domain.Entities;

namespace OnlineBookstore.Service.Contract
{
    public interface IBookservice
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBooksByIdAsync(int id);
        Task AddBooksAsync(Book Books);
        Task UpdateBooksAsync(Book Books);
        Task DeleteBooksAsync(int id);
    }
}
