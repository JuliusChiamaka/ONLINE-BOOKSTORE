using AutoMapper;
using Moq;
using OnlineBookstore.Application.Interfaces.Repository;
using OnlineBookstore.Domain.Dtos.Request;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Infrastructure.Configs;
using OnlineBookstore.Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Test
{
    public class BookServiceTests
    {
        private readonly Mock<IBookRepository> _mockBookRepository;
        private readonly IMapper _mapper;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _mockBookRepository = new Mock<IBookRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = config.CreateMapper();

            _bookService = new BookService(_mockBookRepository.Object, _mapper);
        }

        [Fact]
        public async Task GetAllBooksAsync_ShouldReturnAllBooks()
        {
            // Arrange
            var books = TestData.GetBooks();
            _mockBookRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(books);

            // Act
            var result = await _bookService.GetAllBooksAsync();

            // Assert
            Assert.Equal(books.Count, result.Count());
        }

        [Fact]
        public async Task GetBookByIdAsync_ShouldReturnBook_WhenBookExists()
        {
            // Arrange
            var book = TestData.GetBooks().First();
            _mockBookRepository.Setup(repo => repo.GetByIdAsync(book.Id)).ReturnsAsync(book);

            // Act
            var result = await _bookService.GetBookByIdAsync(book.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(book.Id, result.Id);
        }

        [Fact]
        public async Task GetBookByIdAsync_ShouldReturnNull_WhenBookDoesNotExist()
        {
            // Arrange
            var bookId = 999;
            _mockBookRepository.Setup(repo => repo.GetByIdAsync(bookId)).ReturnsAsync((Book)null);

            // Act
            var result = await _bookService.GetBookByIdAsync(bookId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddBookAsync_ShouldAddBook()
        {
            // Arrange
            var bookRequest = new AddBookRequest
            {
                Title = "New Book",
                Genre = "New Genre",
                ISBN = "1122334455",
                Author = "New Author",
                PublicationYear = 2023,
                Price = 20.0m,
                Description = "New Description"
            };
            var book = _mapper.Map<Book>(bookRequest);

            _mockBookRepository.Setup(repo => repo.AddAsync(It.IsAny<Book>())).Returns(Task.CompletedTask);

            // Act
            await _bookService.AddBookAsync(bookRequest);

            // Assert
            _mockBookRepository.Verify(repo => repo.AddAsync(It.Is<Book>(b => b.Title == book.Title && b.ISBN == book.ISBN)), Times.Once);
        }

        [Fact]
        public async Task UpdateBookAsync_ShouldUpdateBook()
        {
            // Arrange
            var bookRequest = new UpdateBookRequest
            {
                Title = "Updated Book",
                Genre = "Updated Genre",
                ISBN = "1122334455",
                Author = "Updated Author",
                PublicationYear = 2023,
                Price = 20.0m,
                Description = "Updated Description"
            };
            var bookId = 1;
            var book = _mapper.Map<Book>(bookRequest);
            book.Id = bookId;

            _mockBookRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Book>())).Returns(Task.CompletedTask);

            // Act
            await _bookService.UpdateBookAsync(bookId, bookRequest);

            // Assert
            _mockBookRepository.Verify(repo => repo.UpdateAsync(It.Is<Book>(b => b.Id == bookId && b.Title == book.Title && b.ISBN == book.ISBN)), Times.Once);
        }

        [Fact]
        public async Task DeleteBookAsync_ShouldDeleteBook()
        {
            // Arrange
            var bookId = 1;

            _mockBookRepository.Setup(repo => repo.DeleteAsync(bookId)).Returns(Task.CompletedTask);

            // Act
            await _bookService.DeleteBookAsync(bookId);

            // Assert
            _mockBookRepository.Verify(repo => repo.DeleteAsync(bookId), Times.Once);
        }
    }
}
