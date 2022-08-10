using HotChocolate.AspNetCore;
using HotChocolate.Execution;

namespace Demo.Types;

public class CustomHttpRequestInterceptor : DefaultHttpRequestInterceptor
{
    public override ValueTask OnCreateAsync(
        HttpContext context,
        IRequestExecutor requestExecutor,
        IQueryRequestBuilder requestBuilder,
        CancellationToken cancellationToken)
    {
        requestBuilder.TryAddProperty("currentUserId", 69);
        return base.OnCreateAsync(context, requestExecutor, requestBuilder, cancellationToken);
    }
}