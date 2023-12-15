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

        /// <summary>
        /// Returns a collection of books.
        /// </summary>
        IEnumerable<Book> GetAll();

        /// <summary>
        /// Returns an async collection of books.
        /// </summary>
        /// <returns></returns>
        IAsyncEnumerable<Book> GetAllAsAsync();

        /// <summary>
        /// Returns a book by id.
        /// </summary>
        Book? GetBook(int id);

        /// <summary>
        /// Creates a new book.
        /// </summary>
        Task<bool> CreateAsync(Book book);

        /// <summary>
        /// Creates a collection of books in bulk.
        /// </summary>
        Task<bool> CreateAsync(IEnumerable<Book> bookEntities);

        /// <summary>
        /// Returns a collection of books for the given ids.
        /// </summary>
        Task<IEnumerable<Book>> GetAsync(IEnumerable<int> bookIds);
    }
}
