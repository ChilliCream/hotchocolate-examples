namespace Demo.Reviews.Types;

public record Review(int Id, Author Author, Product Product, string Body);
