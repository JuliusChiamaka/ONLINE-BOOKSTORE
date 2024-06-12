using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.Application.Interfaces.Service;
using OnlineBookstore.Domain.Dtos.Request;
using OnlineBookstore.Service.Implementation;

namespace OnlineBookstore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("getBook/{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost("addBook")]
        public async Task<IActionResult> AddBook(AddBookRequest request)
        {
            await _bookService.AddBookAsync(request);
            return Ok(); 
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBook(int id, UpdateBookRequest request)
        {
            await _bookService.UpdateBookAsync(id, request);
            return NoContent();
        }

        [HttpDelete("deleteBook/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks([FromQuery] string title, [FromQuery] string author, [FromQuery] int? year, [FromQuery] string genre)
        {
            var books = await _bookService.SearchBooksAsync(title, author, year, genre);
            return Ok(books);
        }
    }


}
