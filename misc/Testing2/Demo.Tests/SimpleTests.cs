using System.Security.Claims;
namespace Demo.Tests;

public class SimpleTests
{
    [Fact]
    public async Task SchemaChangeTest()
    {
        var schema = await TestServices.Executor.GetSchemaAsync(default);
        schema.MatchSnapshot();
    }

    [Fact]
    public async Task FetchAuthor()
    {
        await using var result = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                """
                {
                  book {
                    author {
                      name
                    } 
                  } 
                }
                """));

        result.MatchInlineSnapshot(
            """
            {
              "data": {
                "book": {
                  "author": {
                    "name": "Jon Skeet"
                  }
                }
              }
            }
            """);
    }
  
    [Fact]
    public async Task FetchAuthorAddress()
    {
        var principal = new ClaimsPrincipal(
            new ClaimsIdentity(
                new[]
                {
                    new Claim("role", "admin")
                }));

        await using var result = await TestServices.ExecuteRequestAsync(
            b => b
              .SetQuery(
                """
                {
                  book {
                    author {
                      address
                    } 
                  } 
                }
                """)
              .AddGlobalState(nameof(ClaimsPrincipal), principal));

        result.MatchInlineSnapshot(
            """
            {
              "data": {
                "book": {
                  "author": {
                    "address": "London"
                  }
                }
              }
            }
            """);
    }

     [Fact]
    public async Task FetchAuthorAddres()
    {
        var principal = new ClaimsPrincipal();

        await using var result = await TestServices.ExecuteRequestAsync(
            b => b
              .SetQuery(
                """
                {
                  book {
                    author {
                      address
                    } 
                  } 
                }
                """)
              .AddGlobalState(nameof(ClaimsPrincipal), principal));

        result.MatchInlineSnapshot(
            """
            {
              "errors": [
                {
                  "message": "The current user is not authorized to access this resource.",
                  "locations": [
                    {
                      "line": 4,
                      "column": 7
                    }
                  ],
                  "path": [
                    "book",
                    "author",
                    "address"
                  ],
                  "extensions": {
                    "code": "AUTH_NOT_AUTHORIZED"
                  }
                }
              ],
              "data": {
                "book": {
                  "author": {
                    "address": null
                  }
                }
              }
            }
            """);
    }

    [Fact]
    public async Task OnPublished()
    {
      var principal = new ClaimsPrincipal(
            new ClaimsIdentity(
                new[]
                {
                    new Claim("role", "admin")
                }));

        using var cts = new CancellationTokenSource(1_000);

        await using var result = await TestServices.ExecuteRequestAsync(
            b => b
              .SetQuery(
                """
                subscription {
                  onPublished {
                    title
                  } 
                }
                """)
              .AddGlobalState(nameof(ClaimsPrincipal), principal));

        var snapshot = new Snapshot();
        var count = 0;

        await foreach(var item in result.ExpectResponseStream()
            .ReadResultsAsync().WithCancellation(cts.Token))
        {
            snapshot.Add(item, $"Result {++count}");

            if(count == 2)
            {
                break;
            } 
        }

        snapshot.Match();
    }

    [Fact]
    public async Task FetchBookAndAuthor()
    {
        var result = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery("{ book { title author { name } } }"));

        result.MatchSnapshot();
    }

    [Fact]
    public async Task FetchAuthor1()
    {
        var result = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery("{ author { name } }"));

        result.MatchSnapshot();
    }
}
