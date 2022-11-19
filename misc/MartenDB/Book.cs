namespace MartenDB;

public class Book
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public bool IsPopular { get; set; }
    public Rating Rating { get; set; }
}

public class Rating
{
    public int Score { get; set; }
}