using Entities;
using Microsoft.EntityFrameworkCore;

namespace DbContexts;

public class BooksDbContext : DbContext
{
    public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    //public DbSet<Author>? Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>().HasData(
            new Author { Id = 1, FirstName = "Author", LastName = "1" },
            new Author { Id = 2, FirstName = "Author", LastName = "2" },
            new Author { Id = 3, FirstName = "Author", LastName = "3" }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "Book 1", AuthorId = 1, Description = "Description 1" },
            new Book { Id = 2, Title = "Book 2", AuthorId = 1, Description = "Description 2" },
            new Book { Id = 3, Title = "Book 3", AuthorId = 2, Description = "Description 3" },
            new Book { Id = 4, Title = "Book 4", AuthorId = 2, Description = "Description 4" },
            new Book { Id = 5, Title = "Book 5", AuthorId = 3, Description = "Description 5" },
            new Book { Id = 6, Title = "Book 6", AuthorId = 3, Description = "Description 6" }
        );

        // modelBuilder.Entity<Book>()
        //     .HasOne(b => b.Author)
        //     .WithMany(a => a.Books)
        //     .HasForeignKey(b => b.AuthorId);
        base.OnModelCreating(modelBuilder);
    }
}