using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Service.Contract;
using OnlineBookstore.Service.Contract.Base;

namespace OnlineBookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookservice _Bookservice;

        public BooksController(IBookservice Bookservice)
        {
            _Bookservice = Bookservice;
        }

        [HttpGet("getAllBooks")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return Ok(await _Bookservice.GetAllBooksAsync());
        }

        [HttpGet("getBook/{id}")]
        public async Task<ActionResult<Book>> GetBooks(int id)
        {
            var Books = await _Bookservice.GetBooksByIdAsync(id);
            if (Books == null)
            {
                return NotFound();
            }
            return Ok(Books);
        }

        [HttpPost("addBook")]
        public async Task<ActionResult> AddBooks([FromBody] Book Books)
        {
            try
            {
                await _Bookservice.AddBooksAsync(Books);
                return CreatedAtAction(nameof(GetBooks), new { id = Books.Id }, Books);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateBook/{id}")]
        public async Task<ActionResult> UpdateBooks(int id, [FromBody] Book Books)
        {
            if (id != Books.Id)
            {
                return BadRequest();
            }

            try
            {
                await _Bookservice.UpdateBooksAsync(Books);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteBook/{id}")]
        public async Task<ActionResult> DeleteBooks(int id)
        {
            try
            {
                await _Bookservice.DeleteBooksAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
