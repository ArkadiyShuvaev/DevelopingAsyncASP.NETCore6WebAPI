namespace Books.Api.Models;

public class BookWithCoversDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public int AuthorId { get; set; }
    public required string AuthorName { get; set; }
    public IEnumerable<BookCoverDto>? BookCovers { get; set; }
}
