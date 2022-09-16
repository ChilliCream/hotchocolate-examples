using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorFileUpload;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddCryptoClient()
    .ConfigureHttpClient(
        c =>
        {
            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes("abc:abc"));
            c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", token);
            c.BaseAddress = new Uri("https://api-crypto-workshop.chillicream.com/graphql");
        });

await builder.Build().RunAsync();
