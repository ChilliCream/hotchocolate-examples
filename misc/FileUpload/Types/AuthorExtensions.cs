using Path = System.IO.Path;
using static FileUpload.Constants;

namespace FileUpload;

[ExtendObjectType(typeof(Author))]
public class AuthorExtensions
{
    public Uri? GetProfilePicture([Parent] Author author)
    {
        if (File.Exists(Path.Combine(ImageDirectory, $"{author.Id}.png")))
        {
            return new Uri($"{ImageRoot}/{author.Id}.png");
        }
        return null;
    }
}
