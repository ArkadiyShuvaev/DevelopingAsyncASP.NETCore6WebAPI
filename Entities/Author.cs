using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

[Table("Authors")]
public class Author
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public ICollection<Book>? Books { get; set; }
}