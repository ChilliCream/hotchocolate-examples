using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StrawberryBuildTests;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services
    .AddCryptoClient()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api-crypto-workshop.chillicream.com/graphql"))
    .ConfigureWebSocketClient(c => c.Uri = new Uri("wss://api-crypto-workshop.chillicream.com/graphql"));

await builder.Build().RunAsync();
