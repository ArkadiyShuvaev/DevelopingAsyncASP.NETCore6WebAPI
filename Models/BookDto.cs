namespace Models;

public class BookDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public Guid AuthorId { get; set; }
    public required string AuthorName { get; set; }
}
