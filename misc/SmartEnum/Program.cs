using System.Diagnostics.CodeAnalysis;
using HotChocolate.Types.Descriptors.Definitions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddEnumType<EmployeeType>(
        d =>
        {
            d.Value(EmployeeType.Manager)
                .Name(EmployeeType.Manager.Name.ToUpperInvariant());
            d.Value(EmployeeType.Assistant)
                .Name(EmployeeType.Assistant.Name.ToUpperInvariant());
            d.Value(EmployeeType.Author)
                .Name(EmployeeType.Author.Name.ToUpperInvariant());
        })
    .AddEnumType(
        d =>
        {
            d.Name("FooType");
            d.Value("foo").Name("FOO");
        });

var app = builder.Build();

app.MapGraphQL();

app.Run();

public class CustomComparer : IEqualityComparer<object>
{
    public new bool Equals(object? x, object? y)
    {
        if (x is string xs && y is string ys)
        {
            return StringComparer.Ordinal.Equals(xs, ys);
        }

        return false;
    }

    public int GetHashCode([DisallowNull] object obj)
    {
        if (obj is string xs)
        {
            return StringComparer.OrdinalOrdinal.GetHashCode(xs);
        }
        return obj.GetHashCode();
    }
}