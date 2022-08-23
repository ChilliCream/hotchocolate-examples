using HotChocolate.AspNetCore;
using HotChocolate.Execution;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .UseInstrumentations()
    .UseExceptions()
    .UseTimeout()
    .UseDocumentCache()
    .UseReadPersistedQuery()
    .UseRequest(next => async context =>
    {
        if ((context.Request.Query is null && (context.IsCachedDocument || context.IsPersistedDocument)) ||
            context.ContextData.ContainsKey("admin"))
        {
            await next(context);
            return;
        }

        var error =
            ErrorBuilder.New()
                .SetMessage("only persisted queries!")
                .Build();

        context.Result = QueryResultBuilder.CreateError(error);
    })
    .UseDocumentParser()
    .UseDocumentValidation()
    .UseOperationCache()
    .UseOperationComplexityAnalyzer()
    .UseOperationResolver()
    .UseOperationVariableCoercion()
    .UseOperationExecution()
    .AddReadOnlyFileSystemQueryStorage("./queries")
    .AddHttpRequestInterceptor<CustomRequestInterceptor>();

var app = builder.Build();

app.MapGraphQL();

app.Run();

public class CustomRequestInterceptor : DefaultHttpRequestInterceptor
{
    public override ValueTask OnCreateAsync(
        HttpContext context,
        IRequestExecutor requestExecutor,
        IQueryRequestBuilder requestBuilder,
        CancellationToken cancellationToken)
    {
        if (context.Request.Headers.ContainsKey("admin"))
        {
            requestBuilder.SetGlobalState("admin", true);
        }

        return base.OnCreateAsync(context, requestExecutor, requestBuilder, cancellationToken);
    }
}