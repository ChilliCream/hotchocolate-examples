using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace Demo.Reviews.Data;

public static class DatabaseHelper
{
    public static async Task SeedDatabaseAsync(WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReviewContext>();
        if (await context.Database.EnsureCreatedAsync())
        {
            var ada = new Author
            {
                Name = "Ada Lovelace"
            };

            var alan = new Author
            {
                Name = "Alan Turing"
            };

            await context.Authors.AddRangeAsync(ada, alan);

            await context.Reviews.AddRangeAsync(
                new Review
                {
                    Body = "Love it!",
                    Stars = 5,
                    ProductId = 1,
                    Author = ada
                },
                new Review
                {
                    Body = "Too expensive.",
                    Stars = 1,
                    ProductId = 2,
                    Author = alan
                },
                new Review
                {
                    Body = "Could be better.",
                    Stars = 3,
                    ProductId = 3,
                    Author = ada,
                },
                new Review
                {
                    Body = "Prefer something else.",
                    Stars = 3,
                    ProductId = 2,
                    Author = alan
                });
            await context.SaveChangesAsync();
        }
    }

}