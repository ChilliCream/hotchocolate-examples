using System.Reflection;
using System.Runtime.CompilerServices;
using HotChocolate.Types.Descriptors;

namespace PreserveParent;

public class PreserveParentAsAttribute : ObjectFieldDescriptorAttribute
{
    public PreserveParentAsAttribute(string name, [CallerLineNumber] int order = 0)
    {
        Order = order;
        Name = name;
    }

    public string Name { get; }

    public override void OnConfigure(
        IDescriptorContext context,
        IObjectFieldDescriptor descriptor,
        MemberInfo member)
    {
        descriptor.Use(next => async context =>
        {
            await next(context);
            context.SetScopedState(Name, context.Result);
        });
    }
}