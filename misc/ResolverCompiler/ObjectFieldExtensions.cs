using System.Reflection;
using HotChocolate.Internal;
using HotChocolate.Types.Descriptors;

namespace Demo.Types;

public static class ObjectFieldExtensions
{
    private static readonly IParameterExpressionBuilder[] _localExpressionBuilders = new[]
    {
        new CustomParameterExpressionBuilder<User?>(c => c.GetLocalValue<User>("user"))
    };

    public static IObjectFieldDescriptor UseSuperUser(
        this IObjectFieldDescriptor descriptor)
    {
        descriptor.Use(next => async context =>
        {
            context.SetLocalValue("user", new User { Id = 1, Name = "Michael" });
            await next(context);
        });

        descriptor.Extend().OnBeforeCompletion(
            static (context, definition) =>
            {
                var compiler = context.DescriptorContext.ResolverCompiler;
                definition.Resolvers =
                    compiler.CompileResolve(
                        definition.Member!,
                        definition.SourceType,
                        definition.ResolverType,
                        _localExpressionBuilders);
            });

        return descriptor;
    }
}

public sealed class UseSuperUserAttribute : ObjectFieldDescriptorAttribute
{
    public override void OnConfigure(
        IDescriptorContext context,
        IObjectFieldDescriptor descriptor,
        MemberInfo member)
        => descriptor.UseSuperUser();
}