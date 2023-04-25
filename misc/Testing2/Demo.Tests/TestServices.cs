namespace Demo.Tests;

public static class TestServices
{
    static TestServices()
    {
        Services = new ServiceCollection()
            .AddLogging()
            .AddAuthorization(
                options =>
                {
                    options.AddPolicy("Admin", policy => policy.RequireClaim("role", "admin"));
                })
            .AddGraphQLServer()
            .AddDemoTypes()
            .AddAuthorization()
            .ModifyRequestOptions(
                options =>
                {
                    options.IncludeExceptionDetails = true;
                })
            .Services
            .AddSingleton(
                sp => new RequestExecutorProxy(
                    sp.GetRequiredService<IRequestExecutorResolver>(),
                    Schema.DefaultName))
            .BuildServiceProvider();

        Executor = Services.GetRequiredService<RequestExecutorProxy>();
    }

    public static IServiceProvider Services { get; }

    public static RequestExecutorProxy Executor { get; }

    public static async Task<IExecutionResult> ExecuteRequestAsync(
        Action<IQueryRequestBuilder> configureRequest,
        CancellationToken cancellationToken = default)
    {
        var scope = Services.CreateAsyncScope();

        var requestBuilder = new QueryRequestBuilder();
        requestBuilder.SetServices(scope.ServiceProvider);
        configureRequest(requestBuilder);
        var request = requestBuilder.Create();

        var result = await Executor.ExecuteAsync(request, cancellationToken);
        result.RegisterForCleanup(scope.DisposeAsync);
        return result;
    }
}