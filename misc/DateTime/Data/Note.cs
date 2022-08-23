namespace Demo.Data;

public class Note
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(1024)]
    public string? Body { get; set; }

    [Required]
    public DateTime Created { get; set; }
}
