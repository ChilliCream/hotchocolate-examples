namespace Demo.Data;

[Index(nameof(Symbol))]
public class Asset
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(5)]
    public string? Symbol { get; set; }

    [Required]
    [MaxLength(128)]
    public string? Name { get; set; }

    [Required]
    [MaxLength(128)]
    public string? Slug { get; set; }

    public string? Description { get; set; }

    [Required]
    [MaxLength(16)]
    public string? Color { get; set; }

    [MaxLength(1024)]
    public string? ImageKey { get; set; }

    [MaxLength(1024)]
    public string? Website { get; set; }

    [MaxLength(1024)]
    public string? WhitePaper { get; set; }

    public AssetPrice? Price { get; set; }
}
