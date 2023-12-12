using Microsoft.AspNetCore.Mvc;

namespace BookCovers.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookCoversController : ControllerBase
{
    BookCoversController()
    {
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookCoverAsync(int id, bool returnFault = false)
    {
        if (returnFault)
        {
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