using Entities;
using Filters;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBooksRepository _booksRepository;

    public BooksController(IBooksRepository booksRepository)
    {
        _booksRepository = booksRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetAllAsync()
    {
        var books = await _booksRepository.GetAllAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    [TypeFilter(typeof(BookResultFilter))]
    public async Task<ActionResult<Book>> GetAsync(int id)
    {
        var book = await _booksRepository.GetAsync(id);
        if (book is null)
        {
            return NotFound();
        }

        return Ok(book);
    }
}