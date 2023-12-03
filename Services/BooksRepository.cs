using DbContexts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class BooksRepository : IBooksRepository
    {
        private readonly BooksDbContext _context;

        public BooksRepository(BooksDbContext context)
        {
            _context = context;
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
    }
}
