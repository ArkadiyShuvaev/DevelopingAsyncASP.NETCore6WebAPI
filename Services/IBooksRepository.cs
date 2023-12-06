using Entities;
using Models;

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

        /// <summary>
        /// Returns a collection of books.
        /// </summary>
        IEnumerable<Book> GetAll();

        /// <summary>
        /// Returns a book by id.
        /// </summary>
        Book? GetBook(int id);

        /// <summary>
        /// Creates a new book.
        /// </summary>
        Task<bool> CreateAsync(Book book);
    }
}
