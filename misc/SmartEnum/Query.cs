using Ardalis.SmartEnum;

namespace HotChocSmartEnums;

public class Query
{
    public Book GetBook() =>
        new Book
        {
            Title = "C# in depth.",
            Author = new Author
            {
                Name = "Jon Skeet"
            }
        };

    public Author GetAuthorByType(EmployeeType type)
    {
        return new Author { Name = "Some Author", Type = type };
    }
}

public class Book
{
    public string Title { get; set; }

    public Author Author { get; set; }
}

public class Author
{
    public string Name { get; set; }

    public EmployeeType Type { get; set; } = EmployeeType.Author;

    [GraphQLType("FooType")]
    public string Foo {get; set;} = "Foo";
}

public abstract class EmployeeType : SmartEnum<EmployeeType>
{
    public static readonly EmployeeType Manager = new ManagerType();
    public static readonly EmployeeType Assistant = new AssistantType();
    public static readonly EmployeeType Author = new AuthorType();

    private EmployeeType(string name, int value) : base(name, value)
    {
    }

    public abstract decimal BonusSize { get; }

    private sealed class ManagerType : EmployeeType
    {
        public ManagerType() : base("Manager", 1) { }

        public override decimal BonusSize => 10_000m;
    }

    private sealed class AssistantType : EmployeeType
    {
        public AssistantType() : base("Assistant", 2) { }

        public override decimal BonusSize => 1_000m;
    }

    private sealed class AuthorType : EmployeeType
    {
        public AuthorType() : base("Author", 3) { }

        public override decimal BonusSize => 5_000m;
    }
}