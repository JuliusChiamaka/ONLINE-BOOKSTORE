using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Service.Contract.Base;
using OnlineBookstore.Service.Contract;

namespace OnlineBookstore.Service.Implementation
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllBooksAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetBookByIdAsync(id);
        }

        public async Task AddBookAsync(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            
            if (string.IsNullOrEmpty(book.Title))
            {
                throw new ArgumentException("Book title cannot be empty");
            }

            await _bookRepository.AddBookAsync(book);
        }

        public async Task UpdateBookAsync(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            
            var existingBook = await _bookRepository.GetBookByIdAsync(book.Id);
            if (existingBook == null)
            {
                throw new KeyNotFoundException("Book not found");
            }

            await _bookRepository.UpdateBookAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            var existingBook = await _bookRepository.GetBookByIdAsync(id);
            if (existingBook == null)
            {
                throw new KeyNotFoundException("Book not found");
            }

            await _bookRepository.DeleteBookAsync(id);
        }
    }
}
