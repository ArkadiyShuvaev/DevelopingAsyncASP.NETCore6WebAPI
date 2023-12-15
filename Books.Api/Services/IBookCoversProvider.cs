using Books.Api.Models;

namespace Books.Api.Services;

public interface IBookCoversProvider
{
    Task<BookCoverDto?> GetBookCoverAsync(int id);
}