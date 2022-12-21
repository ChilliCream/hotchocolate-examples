using System.Runtime.CompilerServices;
using System.Security.Claims;
using HotChocolate.Execution;

namespace AuditExample.Types;

public sealed class AuditMiddleware
{
    private readonly HotChocolate.Execution.RequestDelegate _next;
    private readonly IAuditService _auditService;

    public AuditMiddleware(
        HotChocolate.Execution.RequestDelegate next,
        IAuditService auditService)
    {
        _next = next;
        _auditService = auditService;
    }

    public async ValueTask InvokeAsync(IRequestContext context)
    {
        await _next(context);

        if (context.Result is IQueryResult queryResult &&
            queryResult.ContextData is not null &&
            queryResult.ContextData.TryGetValue("categories", out var value) &&
            value is HashSet<string> categories)
        {
            _auditService.ReportUsage(GetUser(context), categories);
        }
        else if (context.Result is IResponseStream stream)
        {
            var user = GetUser(context);
            var wrapperStream = new ResponseStream(
                () => CreateStreamAsync(stream, user),
                stream.Kind,
                stream.ContextData);
            wrapperStream.RegisterForCleanup(stream);
            context.Result = wrapperStream;
        }
    }

    private async IAsyncEnumerable<IQueryResult> CreateStreamAsync(
        IResponseStream originalStream,
        ClaimsPrincipal user,
        [EnumeratorCancellation] CancellationToken ct = default)
    {
        var categories = new HashSet<string>();

        await foreach (var queryResult in originalStream.ReadResultsAsync().WithCancellation(ct))
        {
            if (categories.Count > 0)
            {
                categories = new HashSet<string>();
            }

            if (queryResult.ContextData is not null &&
                queryResult.ContextData.TryGetValue("categories", out var value) &&
                value is HashSet<string> cats)
            {
                foreach (string categoryName in cats)
                {
                    categories.Add(categoryName);
                }
            }

            if (queryResult.Incremental is not null)
            {
                foreach (IQueryResult incremental in queryResult.Incremental)
                {
                    if (incremental.ContextData is not null &&
                        incremental.ContextData.TryGetValue("categories", out value) &&
                        value is HashSet<string> catsIncr)
                    {
                        foreach (string categoryName in catsIncr)
                        {
                            categories.Add(categoryName);
                        }
                    }
                }
            }

            if (categories.Count > 0)
            {
                _auditService.ReportUsage(user, categories);
            }

            yield return queryResult;
        }
    }

    private ClaimsPrincipal GetUser(IRequestContext context)
        => (ClaimsPrincipal)context.ContextData[nameof(ClaimsPrincipal)]!;
}