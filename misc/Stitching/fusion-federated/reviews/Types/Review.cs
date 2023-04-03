namespace Demo.Reviews.Types;

public record Review(int Id, User User, Product Product, string Body);
