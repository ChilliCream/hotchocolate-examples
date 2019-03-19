using System;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Resolvers;

namespace Instrumentation
{
    public class Query
    {
        public string Hello() => "world";
        public string ErrorWithException() => throw new ArgumentException("Foo");
        public string Error(IResolverContext context)
        {
            throw new QueryException(ErrorBuilder.New()
                .SetCode("FooBar")
                .SetPath(context.Path)
                .AddLocation(context.FieldSelection)
                .SetMessage("MyMessage")
                .Build());
        }
    }
}