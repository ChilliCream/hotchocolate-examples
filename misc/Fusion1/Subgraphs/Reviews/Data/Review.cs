namespace Demo.Reviews.Data;

[Index(nameof(ProductId))]
public class Review
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(1024)]
    public string? Body { get; set; }

    [Required]
    public int Stars { get; set; }

    [Required]
    public int ProductId { get; set; }

    public int AuthorId { get; set; }

    [Required]
    public Author? Author { get; set; }
}
