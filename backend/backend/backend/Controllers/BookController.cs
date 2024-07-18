using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // GET: api/books
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return DataStore.Books;
        }

        // GET: api/books/5
        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            var book = DataStore.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        // POST: api/books
        [HttpPost]
        public ActionResult Post([FromBody] Book book)
        {
            book.Id = DataStore.Books.Count + 1; // Simulate auto-increment id
            DataStore.Books.Add(book);
            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
        }

        // PUT: api/books/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book book)
        {
            var existingBook = DataStore.Books.FirstOrDefault(b => b.Id == id);
            if (existingBook == null)
            {
                return NotFound();
            }

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.ISBN = book.ISBN;
            existingBook.PublicationDate = book.PublicationDate;

            return NoContent();
        }

        // DELETE: api/books/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var bookToRemove = DataStore.Books.FirstOrDefault(b => b.Id == id);
            if (bookToRemove == null)
            {
                return NotFound();
            }

            DataStore.Books.Remove(bookToRemove);
            return NoContent();
        }
    }
}