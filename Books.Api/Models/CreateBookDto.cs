using System.ComponentModel.DataAnnotations;

namespace Models;

public class CreateBookDto
{
    [Required]
    [StringLength(150, MinimumLength = 1)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(2500, MinimumLength = 1)]
    public string Description { get; set; } = null!;

    [Required]
    public int AuthorId { get; set; }
}
