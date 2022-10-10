using System.Dynamic;
using System.Reflection;
using System.Text;
using System.Windows.Markup;
using HotChocolate.Resolvers;
using HotChocolate.Types.Descriptors;

namespace InputFormatterDemo;

public class Query
{
    [Base64]
    public string EncodeId(EncodedInput input)
    {
        return input.S;
    }
}

public record EncodedInput([property: Base64] string S);

public static class Extensions
{
    private static readonly IdFormatter _formatter = new();

    public static IObjectFieldDescriptor ToBase64(
        this IObjectFieldDescriptor descriptor)
    {
        var def = descriptor.Extend().Definition;
        def.FormatterDefinitions.Add(new((c, r) => _formatter.FormatResult(c, r)));
        return descriptor;
    }

    public static IArgumentDescriptor FromBase64(
        this IArgumentDescriptor descriptor)
    {
        var def = descriptor.Extend().Definition;
        def.Formatters.Add(_formatter);
        return descriptor;
    }

    public static IInputFieldDescriptor FromBase64(
        this IInputFieldDescriptor descriptor)
    {
        var def = descriptor.Extend().Definition;
        def.Formatters.Add(_formatter);
        return descriptor;
    }
}

[AttributeUsage(
    AttributeTargets.Method |
    AttributeTargets.Property |
    AttributeTargets.Parameter)]
public sealed class Base64Attribute : DescriptorAttribute
{
    protected override void TryConfigure(
        IDescriptorContext context,
        IDescriptor descriptor,
        ICustomAttributeProvider element)
    {
        switch (descriptor)
        {
            case IObjectFieldDescriptor desc:
                desc.ToBase64();
                break;

            case IArgumentDescriptor desc:
                desc.FromBase64();
                break;

            case IInputFieldDescriptor desc:
                desc.FromBase64();
                break;
        }
    }
}

public sealed class IdFormatter : IInputValueFormatter
{
    public object? Format(object? runtimeValue)
    {
        if (runtimeValue is string s)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(s));
        }
        return runtimeValue;
    }

    public object? FormatResult(IPureResolverContext context, object? result)
    {
        if (result is string s)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
        }
        return result;
    }
}