using Moq;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Service.Contract.Base;
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
        private readonly Mock<IBookRepository> _mockRepo;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _mockRepo = new Mock<IBookRepository>();
            _bookService = new BookService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllBooksAsync_ReturnsBooks()
        {
            _mockRepo.Setup(repo => repo.GetAllBooksAsync()).ReturnsAsync(new List<Book> { new Book { Id = 1, Title = "Test Book" } });

            var result = await _bookService.GetAllBooksAsync();

            var resultList = result.ToList();
            Assert.Single(resultList);
            Assert.Equal("Test Book", resultList[0].Title);
        }

        [Fact]
        public async Task AddBookAsync_ThrowsArgumentNullException_WhenBookIsNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _bookService.AddBookAsync(null));
        }

       
    }
}
