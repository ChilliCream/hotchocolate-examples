var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthorization(
        options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireClaim("role", "admin"));
        });

builder.Services
    .AddGraphQLServer()
    .AddDemoTypes()
    .AddAuthorization();

var app = builder.Build();

app.MapGraphQL();

app.Run();
