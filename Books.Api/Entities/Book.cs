using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

[Table("Books")]
public class Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public required string Title { get; set; }

    [MaxLength(2500)]
    public required string Description { get; set; }

    public int AuthorId { get; set; }

    [ForeignKey(nameof(AuthorId))]
    public Author Author { get; set; } = null!;
}
