namespace Demo.Data;

[Index(nameof(Symbol))]
[Index(nameof(TradableMarketCapRank))]
public class AssetPrice
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(5)]
    public string? Symbol { get; set; }

    [Required]
    [MaxLength(5)]
    public string? Currency { get; set; }

    [Required]
    public double LastPrice { get; set; }

    [Required]
    public double MarketCap { get; set; }

    [Required]
    public double TradableMarketCapRank { get; set; }

    [Required]
    public double Volume24Hour { get; set; }

    [Required]
    public double VolumePercentChange24Hour { get; set; }

    [Required]
    public double CirculatingSupply { get; set; }

    [Required]
    public double MaxSupply { get; set; }

    [Required]
    public double High24Hour { get; set; }

    [Required]
    public double Low24Hour { get; set; }

    [Required]
    public double Open24Hour { get; set; }

    [Required]
    public double TradingActivity { get; set; }

    [Required]
    public double Change24Hour { get; set; }

    [Required]
    public int AssetId { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Asset? Asset { get; set; }
}