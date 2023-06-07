using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Reviews.Data;

[Table("Users")]
public class Author
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(128)]
    public string? Name { get; set; }

    public IList<Review> Reviews { get; set; } = new List<Review>();
}
