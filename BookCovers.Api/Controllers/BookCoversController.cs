using Microsoft.AspNetCore.Mvc;

namespace BookCovers.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookCoversController : ControllerBase
{
    private readonly ILogger<BookCoversController> _logger;

    public BookCoversController(ILogger<BookCoversController> logger)
    {
        _logger = logger;
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookCoverAsync(int id, bool returnFault = false)
    {
        _logger.LogInformation("Getting book cover {id}.", id);

        if (returnFault)
        {
            _logger.LogWarning("Returning a fake error for book cover {id}.", id);

            await Task.Delay(100);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        var random = new Random();
        int fakeCoverBytes = random.Next(5097152, 10485760);
        byte[] fakeCover = new byte[fakeCoverBytes];
        random.NextBytes(fakeCover);

        return Ok(new {
            Id = id,
            Cover = fakeCover
        });
    }
}