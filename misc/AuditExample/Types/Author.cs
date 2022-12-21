using System.Reflection;
using HotChocolate.Resolvers;
using HotChocolate.Types.Descriptors;
using HotChocolate.Types.Descriptors.Definitions;

namespace AuditExample.Types;

public record Author(string Name);

[Category("Personal")]
[ExtendObjectType(nameof(Author))]
public static class AuthorPersonalData
{
    public static string Address => "Here is my address!";

    [Category("Location")]
    public static string City => "This is the city";
}

[AttributeUsage(AttributeTargets.Class |
    AttributeTargets.Property |
    AttributeTargets.Method)]
public class CategoryAttribute : DescriptorAttribute
{
    public CategoryAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }

    protected override void TryConfigure(
        IDescriptorContext context,
        IDescriptor descriptor,
        ICustomAttributeProvider element)
    {
        if (descriptor is IObjectTypeDescriptor typeDesc)
        {
            typeDesc.Extend().OnBeforeNaming((_, d) =>
            {
                foreach (var field in d.Fields)
                {
                    field.MiddlewareDefinitions.Add(
                        new FieldMiddlewareDefinition(
                            next => async ctx =>
                            {
                                TrackCategory(ctx, Name);
                                await next(ctx);
                            },
                            isRepeatable: false,
                            key: "CategoryMiddleware"));
                }
            });
        }
        else if (descriptor is IObjectFieldDescriptor fieldDesc)
        {
            fieldDesc.Extend().Definition.MiddlewareDefinitions.Add(
                new FieldMiddlewareDefinition(
                    next => async ctx =>
                    {
                        TrackCategory(ctx, Name);
                        await next(ctx);
                    },
                    isRepeatable: false,
                    key: "CategoryMiddleware"));
        }
    }

    private static void TrackCategory(
        IMiddlewareContext context,
        string categoryName)
    {
        context.OperationResult.SetResultState(
            "categories",
            categoryName,
            static (_, currentValue, c) =>
            {
                if (currentValue is not HashSet<string> categories)
                {
                    categories = new HashSet<string>();
                }
                categories.Add(c);
                return categories;
            });
    }
}