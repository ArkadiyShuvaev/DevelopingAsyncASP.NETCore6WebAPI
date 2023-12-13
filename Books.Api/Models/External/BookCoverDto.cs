namespace Books.Api.Models.External
{
    public class BookCoverDto
    {
        public int Id { get; set; }
        public required byte[] Cover { get; set; }
    }
}
