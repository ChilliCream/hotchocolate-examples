using Path = System.IO.Path;
using static FileUpload.Constants;

namespace FileUpload;

public class Query
{
    public Book GetBook()
        => new Book("C# in depth.", new Author(1, "Jon Skeet"));
}

public class Mutation
{
    public async Task<Author> UploadProfilePicture(int authorId, IFile file, CancellationToken cancellationToken)
    {
        using var stream = File.Create(Path.Combine(ImageDirectory, $"{authorId}.png"));
        await file.CopyToAsync(stream, cancellationToken);
        return new Author(authorId, "Jon Skeet");
    }
}

public record Book(string Title, Author Author);

public record Author(int Id, string Name);
