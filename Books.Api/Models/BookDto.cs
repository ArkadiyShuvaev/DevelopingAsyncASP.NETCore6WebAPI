using Books.Api.Models;

namespace Models;

public class BookDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public int AuthorId { get; set; }
    public required string AuthorName { get; set; }
    public BookCoverDto[]? BookCovers { get; set; }
}
