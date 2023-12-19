namespace Books.Api.Models.External;

public class BookCoverResponse
{
    public int Id { get; set; }
    public required byte[] Cover { get; set; }
}
