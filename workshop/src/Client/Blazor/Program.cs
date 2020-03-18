using Internal;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using Client.Extensions;
using Client.Services;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using StrawberryShake.Transport.WebSockets;

namespace Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddBlazoredSessionStorage();
            builder.Services.AddHttpClient(
                "ChatClient",
                (services, client) =>
                {
                    var token = services.GetRequiredService<ITokenStore>().GetToken();
                    client.BaseAddress = new Uri("http://localhost:5000/");
                    client.AddBearerToken(token);
                });
            builder.Services.AddWebSocketClient(
                "ChatClient",
                (services, client) =>
                {
                    var token = services.GetRequiredService<ITokenStore>().GetToken();
                    client.Uri = new Uri("ws://localhost:5000?token=" + token);
                });
            builder.Services.AddChatClient();
            builder.Services.AddTokenServices();
            builder.Services.AddTypingTracking();
            builder.RootComponents.Add<App>("app");

            await builder.Build().RunAsync();
        }
    }
}
