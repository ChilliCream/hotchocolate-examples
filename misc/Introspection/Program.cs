using HotChocolate.AspNetCore;
using HotChocolate.Execution;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddIntrospectionAllowedRule()
    .AddHttpRequestInterceptor<CustomHttpRequestInterceptor>();

var app = builder.Build();

app.MapGraphQLHttp("/foo").WithOptions(new );
app.MapGraphQLSchema("/graphql/schema").RequireAuthorization();
app.MapBananaCakePop("/graphql/ui", relativeRequestPath: "../../foo");

app.Run();

public class CustomHttpRequestInterceptor : DefaultHttpRequestInterceptor
{
    public override ValueTask OnCreateAsync(
        HttpContext context, 
        IRequestExecutor requestExecutor, 
        IQueryRequestBuilder requestBuilder, 
        CancellationToken cancellationToken)
    {
        if(context.Request.Headers.ContainsKey("introspection"))
        {
            requestBuilder.AllowIntrospection();
        }

        return base.OnCreateAsync(context, requestExecutor, requestBuilder, cancellationToken);
    }
}