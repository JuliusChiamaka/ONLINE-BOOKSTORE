using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Service.Contract.Base;
using OnlineBookstore.Service.Contract;

namespace OnlineBookstore.Service.Implementation
{
    public class Bookservice : IBookservice
    {
        private readonly IBooksRepository _BooksRepository;

        public Bookservice(IBooksRepository BooksRepository)
        {
            _BooksRepository = BooksRepository;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _BooksRepository.GetAllBooksAsync();
        }

        public async Task<Book> GetBooksByIdAsync(int id)
        {
            return await _BooksRepository.GetBooksByIdAsync(id);
        }

        public async Task AddBooksAsync(Book Books)
        {
            if (Books == null)
            {
                throw new ArgumentNullException(nameof(Books));
            }

            
            if (string.IsNullOrEmpty(Books.Title))
            {
                throw new ArgumentException("Books title cannot be empty");
            }

            await _BooksRepository.AddBooksAsync(Books);
        }

        public async Task UpdateBooksAsync(Book Books)
        {
            if (Books == null)
            {
                throw new ArgumentNullException(nameof(Books));
            }

            
            var existingBooks = await _BooksRepository.GetBooksByIdAsync(Books.Id);
            if (existingBooks == null)
            {
                throw new KeyNotFoundException("Books not found");
            }

            await _BooksRepository.UpdateBooksAsync(Books);
        }

        public async Task DeleteBooksAsync(int id)
        {
            var existingBooks = await _BooksRepository.GetBooksByIdAsync(id);
            if (existingBooks == null)
            {
                throw new KeyNotFoundException("Books not found");
            }

            await _BooksRepository.DeleteBooksAsync(id);
        }
    }
}
