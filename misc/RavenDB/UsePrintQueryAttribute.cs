using System;
using System.Reflection;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using Raven.Client.Documents.Linq;

namespace raven;

public sealed class UsePrintQueryAttribute : ObjectFieldDescriptorAttribute
{
    protected override void OnConfigure(
        IDescriptorContext context, IObjectFieldDescriptor descriptor, MemberInfo member)
    {
        descriptor.Use(next => async (context) =>
        {
            await next(context);
            if (context.Result is IRavenQueryable<Query> q)
            {
                Console.WriteLine(q.ToString());
            }
            else if (context.Result is IExecutable e)
            {
                Console.WriteLine(e.Print());
            }
        });
    }
}
