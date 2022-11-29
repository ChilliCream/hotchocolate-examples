namespace PreserveParent;

[ExtendObjectType<Author>]
public class AuthorResolvers
{
    public string NameAndTitle([Parent] Author author, [ScopedState] Book book) 
        => author.Name + " - " + book.Title;

    /*
        // create from the path structure
        public string NameAndTitle([ScopedState] ImmutableStack<object> path)
        {
            path = path.Pop(out var author);
            path.Pop(out var book);

            return ((Author)author).Name + " - " + ((Book)book).Title;
        }
    */
}
