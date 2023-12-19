using Books.Api.Models.External;

namespace Books.Api.Services;

public interface IBookCoversProvider
{
    Task<BookCoverResponse?> GetBookCoverAsync(int id);

    /// <summary>
    /// Returns a collection of book covers one by one.
    /// </summary>
    Task<IEnumerable<BookCoverResponse>> GetBookCoversProcessOneByOneAsync();
}