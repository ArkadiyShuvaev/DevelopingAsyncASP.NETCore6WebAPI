using DbContexts;
using Entities;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services
{
    public class BooksRepository : IBooksRepository
    {
        private readonly BooksDbContext _context;

        public BooksRepository(BooksDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book.Id > 1;
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books.Include(b => b.Author);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.Include(b => b.Author)
                                       .ToListAsync();
        }

        public async Task<Book?> GetAsync(int id)
        {
            return await _context.Books.Include(b => b.Author)
                                       .FirstOrDefaultAsync(b => b.Id == id);
        }

        public Book? GetBook(int id)
        {
            return _context.Books.Include(b => b.Author)
                                 .FirstOrDefault(b => b.Id == id);
        }
    }
}
