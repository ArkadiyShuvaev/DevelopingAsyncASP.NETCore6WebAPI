using System.Net;
using System.Linq;
using AutoMapper;
using Entities;
using Filters;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using Books.Api.Services;
using Books.Api.Models;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBooksRepository _booksRepository;
    private readonly IMapper _mapper;
    private readonly IBookCoversProvider _bookCoversProvider;
    private readonly ILogger<BooksController> _logger;

    public BooksController(IBooksRepository booksRepository,
                           IMapper mapper,
                           IBookCoversProvider bookCoversProvider,
                           ILogger<BooksController> logger)
    {
        _booksRepository = booksRepository;
        _mapper = mapper;
        _bookCoversProvider = bookCoversProvider;
        _logger = logger;
    }

    [HttpGet]
    [TypeFilter(typeof(BookResultFilter))]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetAllAsync()
    {
        var books = await _booksRepository.GetAllAsync();
        return Ok(books);
    }

    [HttpGet("get-stream")]
    public async IAsyncEnumerable<BookDto> GetBookStreamAsync()
    {
        // For the sake of testing the library System.Linq.Async
        var books = _booksRepository.GetAllAsAsync().Where(x => x.Id > 0);
        await foreach (var book in books)
        {
            // Add a delay to visually see the effect of streaming
            await Task.Delay(500);

            var bookDto = _mapper.Map<BookDto>(book);
            Console.WriteLine($"Sending book {bookDto.Title}.");
            yield return bookDto;
        }
    }

    [HttpGet("{id}", Name = "GetBook")]
    public async Task<ActionResult<BookDto>> GetAsync(int id)
    {
        _logger.LogInformation("The thread id of the method '{MethodName}' is '{ThreadId}'.",
            nameof(GetAsync),
            Thread.CurrentThread.ManagedThreadId);

        var book = await _booksRepository.GetAsync(id);
        if (book is null)
        {
            return NotFound();
        }

        var bookCover = await _bookCoversProvider.GetBookCoverAsync(book.Id);
        var bookDto = _mapper.Map<BookDto>(book);
        var amountOfPages = await GetAmountOfPages_WrongApproach(id);

        if (bookCover is not null)
        {
            var bookCoverDto = _mapper.Map<BookCoverDto>(bookCover);
            bookDto.BookCovers = new[] { bookCoverDto };
        }

        return Ok(bookDto);
    }

    private Task<int> GetAmountOfPages_WrongApproach(int id)
    {
        return Task.Run(() =>
        {
            _logger.LogInformation("The thread id of the method '{MethodName}' is '{ThreadId}'.",
                nameof(GetAmountOfPages_WrongApproach),
                Thread.CurrentThread.ManagedThreadId);

            return Books.Legacy.PageCalculator.CalculateBookPages(id);
        });
    }

    [HttpPost]
    [TypeFilter(typeof(BookResultFilter))]
    public async Task<ActionResult<Book>> CreateAsync([FromBody] CreateBookDto createBook)
    {
        var bookEntity = _mapper.Map<Book>(createBook);
        await _booksRepository.CreateAsync(bookEntity);

        var book = await _booksRepository.GetAsync(bookEntity.Id);
        return StatusCode((int)HttpStatusCode.Created, book);
    }

    [HttpPost("bulk")]
    public async Task<ActionResult<IEnumerable<Book>>> CreateBulkAsync([FromBody] IEnumerable<CreateBookDto> createBooks)
    {
        var bookEntities = _mapper.Map<IEnumerable<Book>>(createBooks);
        await _booksRepository.CreateAsync(bookEntities);

        var bookIds = bookEntities.Select(b => b.Id).ToList();
        return StatusCode((int)HttpStatusCode.Created, bookIds);
    }

    [HttpGet("bulk", Name = nameof(GetBulkAsync))]
    [TypeFilter(typeof(BookResultFilter))]
    public async Task<ActionResult<Book>> GetBulkAsync([FromQuery] IEnumerable<int> bookIds, CancellationToken ct)
    {
        try
        {
            var entities = await _booksRepository.GetAsync(bookIds);
            if (entities?.Count() != bookIds.Count())
            {
                return NotFound();
            }

            var bookCovers = await _bookCoversProvider.GetBookCoversProcessOneByOneAsync(bookIds, ct);

            return Ok(entities);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}