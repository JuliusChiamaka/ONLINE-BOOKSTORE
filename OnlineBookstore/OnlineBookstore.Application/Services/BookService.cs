using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Service.Contract;
using AutoMapper;
using OnlineBookstore.Domain.Dtos.Request;
using OnlineBookstore.Domain.Dtos.Response;
using OnlineBookstore.Application.Interfaces.Repository;
using OnlineBookstore.Application.Interfaces.Service;

namespace OnlineBookstore.Service.Implementation
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookResponse>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookResponse>>(books);
        }

        public async Task<BookResponse> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return _mapper.Map<BookResponse>(book);
        }

        public async Task AddBookAsync(AddBookRequest request)
        {
            var book = _mapper.Map<Book>(request);
            await _bookRepository.AddAsync(book);
        }

        public async Task UpdateBookAsync(int id, UpdateBookRequest request)
        {
            var book = _mapper.Map<Book>(request);
            book.Id = id;
            await _bookRepository.UpdateAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            await _bookRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<BookResponse>> SearchBooksAsync(string title, string author, int? year, string genre)
        {
            var books = await _bookRepository.SearchAsync(title, author, year, genre);
            return _mapper.Map<IEnumerable<BookResponse>>(books);
        }
    }
}
