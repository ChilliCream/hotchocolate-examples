using HotChocolate.Internal;
using HotChocolate.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPooledDbContextFactory<AssetContext>(o => o.UseSqlite("Data Source=assets.db"));

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .RegisterDbContext<AssetContext>(DbContextKind.Pooled)
    .AddHttpRequestInterceptor<CustomHttpRequestInterceptor>();

builder.Services
    .AddSingleton<IParameterExpressionBuilder>(
        new CustomParameterExpressionBuilder<int?>(
            c => c.GetGlobalValue<int?>("currentUserId"),
            p => p.Name.EqualsOrdinal("currentUserId")));

builder.Services
    .AddSingleton<IParameterExpressionBuilder, CurrentUserParameterExpressionBuilder>();

var app = builder.Build();

app.MapGraphQL();

app.Run();
