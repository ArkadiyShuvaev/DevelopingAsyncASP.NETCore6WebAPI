using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksSyncronousController : ControllerBase
{
    private readonly IBooksRepository _booksRepository;

    public BooksSyncronousController(IBooksRepository booksRepository)
    {
        _booksRepository = booksRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Book>> GetAll()
    {
        var books = _booksRepository.GetAll();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public ActionResult<Book> Get(int id)
    {
        var book = _booksRepository.GetBook(id);
        if (book is null)
        {
            return NotFound();
        }

        return Ok(book);
    }
}