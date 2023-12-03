using Entities;

namespace Services
{
    public interface IBooksRepository
    {
        /// <summary>
        /// Returns a collection of books.
        /// </summary>
        Task<IEnumerable<Book>> GetAllAsync();

        /// <summary>
        /// Returns a book by id.
        /// </summary>
        Task<Book?> GetAsync(int id);

        // Task<Book?> GetBookAsync(int id);
        // Task<Book?> AddBookAsync(Book book);
        // Task<Book?> UpdateBookAsync(Book book);
        // Task<Book?> DeleteBookAsync(int id);
    }
}
