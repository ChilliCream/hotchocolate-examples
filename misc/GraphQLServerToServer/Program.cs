using Microsoft.AspNetCore.Mvc;
using ServerToServer;
using StrawberryShake;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCryptoClient()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api-crypto-workshop.chillicream.com/graphql"));

var app = builder.Build();

app.MapGet(
    "/{symbol}", 
    async (string symbol, [FromServices] GetAssetBySymbolQuery query, CancellationToken cancellationToken) => 
    {
        var result = await query.ExecuteAsync(symbol, cancellationToken);
        result.EnsureNoErrors();
        return result.Data!.AssetBySymbol;
    });

app.Run();
