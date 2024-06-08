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
    public class BookserviceTests
    {
        private readonly Mock<IBooksRepository> _mockRepo;
        private readonly Bookservice _Bookservice;

        public BookserviceTests()
        {
            _mockRepo = new Mock<IBooksRepository>();
            _Bookservice = new Bookservice(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllBooksAsync_ReturnsBooks()
        {
            _mockRepo.Setup(repo => repo.GetAllBooksAsync()).ReturnsAsync(new List<Book> { new Book { Id = 1, Title = "Test Books" } });

            var result = await _Bookservice.GetAllBooksAsync();

            var resultList = result.ToList();
            Assert.Single(resultList);
            Assert.Equal("Test Books", resultList[0].Title);
        }

        [Fact]
        public async Task AddBooksAsync_ThrowsArgumentNullException_WhenBooksIsNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _Bookservice.AddBooksAsync(null));
        }

       
    }
}
