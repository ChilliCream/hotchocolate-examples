using System.Text.Encodings.Web;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Subscriptions;
using HotChocolate.AspNetCore.Subscriptions.Protocols;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.Extensions.Options;
using static AuthenticationSocketSessionInterceptor;

var builder = WebApplication.CreateBuilder(args);

// 1) Add GraphQL to the builder, including the custom Socket Session Interceptor
//    which will intercept the 'connection_init' message of the WebSocket connection.
builder
    .AddGraphQL()
    .AddSocketSessionInterceptor<AuthenticationSocketSessionInterceptor>()
    .AddTypes();

// 2) We define two scheme names for our authentication setup:
//    - DefaultScheme: The normal JWT Bearer scheme
//    - WebsocketScheme: A special "stub" scheme for WebSocket handshake
const string DefaultScheme = nameof(DefaultScheme);
const string WebsocketScheme = nameof(WebsocketScheme);

// 3) Configure authentication services
builder.Services.AddAuthentication(options =>
    {
        // The default scheme to use when the application attempts authentication
        options.DefaultScheme = DefaultScheme;
        options.DefaultAuthenticateScheme = DefaultScheme;
    })
    // Add the "stub" scheme (WebsocketScheme) which will skip authentication 
    // during the initial WebSocket Upgrade request.
    .AddScheme<AuthenticationSchemeOptions, SkipWebSocketAuthenticationHandler>(WebsocketScheme, _ => { })
    // Add the actual JWT Bearer scheme (DefaultScheme).
    // This scheme will normally handle all requests unless we forward them to the stub.
    .AddJwtBearer(DefaultScheme, x =>
    {
        // Set Authority and Audience for validating the JWT 
        // (using DuendeSoftware's demo endpoint here).
        x.Authority = "https://demo.duendesoftware.com";
        x.Audience = "api";

        // ForwardDefaultSelector decides at runtime whether to use 
        // the DefaultScheme or the WebsocketScheme.
        x.ForwardDefaultSelector = context =>
        {
            // Check if the interceptor has already placed a token in the Items collection 
            // (meaning we already handled the WebSocket handshake).
            var hasTokenFromWebsocket = context.Items.ContainsKey(WEBSOCKET_TOKEN);

            // If no token is in the context and it's an Upgrade request (WebSocket),
            // we forward to our "stub" WebsocketScheme to skip normal JWT checking.
            if (!hasTokenFromWebsocket && context.WebSockets.IsWebSocketRequest)
            {
                return WebsocketScheme;
            }

            // Otherwise, use the normal DefaultScheme (JWT Bearer).
            return DefaultScheme;
        };

        // Set up event handlers for the JWT Bearer scheme.(otherwise you get a NullReferenceException)
        x.Events ??= new();
        x.Events.OnMessageReceived = context =>
        {
            // If we stored a token in the HttpContext during the WebSocket handshake,
            // retrieve it here and set context.Token so that 
            // the JWT Bearer logic can proceed using that token.
            if (context.HttpContext.Items.TryGetValue(WEBSOCKET_TOKEN, out var token))
            {
                context.Token = token as string;
            }

            return Task.CompletedTask;
        };
    });

// 4) Add WebSockets support, needed to accept WebSocket upgrade requests
builder.Services.AddWebSockets(_ => { });

// 5) For demonstration, we add an HTTP client that can fetch tokens from the Duende demo server
builder.Services.AddHttpClient("token");

var app = builder.Build();

// 6) Enable WebSockets in the request pipeline
app.UseWebSockets();

// 7) Enable authentication for all incoming requests
app.UseAuthentication();

// 8) Add routing 
app.UseRouting();

// 9) Map the GraphQL endpoint so the server can serve GraphQL queries and subscriptions
app.MapGraphQL();

// 10) A simple endpoint to request a token from the Duende demo server
app.MapGet("/token",
    async (IHttpClientFactory factory) =>
    {
        using var client = factory.CreateClient("token");
        client.BaseAddress = new Uri("https://demo.duendesoftware.com/connect/token");

        // Request a client credentials token from the demo endpoint
        var response = await client.RequestClientCredentialsTokenAsync(
            new ClientCredentialsTokenRequest
            {
                ClientId = "m2m",
                ClientSecret = "secret",
                Scope = "api"
            });

        return response;
    });

// 11) Start the application, allowing additional GraphQL commands if provided 
app.RunWithGraphQLCommands(args);

/// <summary>
/// This handler is linked to our special "WebsocketScheme". 
/// It's responsible for skipping or returning no authentication result 
/// when the request is an Upgrade request for a WebSocket.
/// </summary>
public sealed class SkipWebSocketAuthenticationHandler :
    AuthenticationHandler<AuthenticationSchemeOptions>
{
    [Obsolete("Obsolete")]
    public SkipWebSocketAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
    }

    public SkipWebSocketAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder) : base(options, logger, encoder)
    {
    }

    /// <summary>
    /// This method is called to handle authentication.
    /// If it's a WebSocket upgrade request, we do nothing (NoResult).
    /// Otherwise, we fail the authentication.
    /// </summary>
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // If the request is a WebSocket upgrade request,
        // return "NoResult" to skip normal authentication checks.
        if (Context.WebSockets.IsWebSocketRequest)
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        // Otherwise, we fail authentication for non-WebSocket requests.
        return Task.FromResult(AuthenticateResult.Fail("Authentication not applicable"));
    }
}

/// <summary>
/// This interceptor is used to handle the 'connection_init' message 
/// of the WebSocket GraphQL subscription. We read the token from the payload
/// and try to authenticate again using the default (JWT) scheme.
/// </summary>
public sealed class AuthenticationSocketSessionInterceptor
    : DefaultSocketSessionInterceptor
{
    // A key used to store the WebSocket token in the HttpContext.
    public static readonly string WEBSOCKET_TOKEN = "websocket-auth-token";

    // The key in the connection_init message that the client uses to 
    // send the token (e.g., { authorization: "Bearer <token>" }).
    public static readonly string WEBOCKET_PAYLOAD_AUTH_KEY = "authorization";

    /// <summary>
    /// Called when the client sends the 'connection_init' message.
    /// We check the payload for a token, store it, and re-run the authentication.
    /// </summary>
    public override async ValueTask<ConnectionStatus> OnConnectAsync(
        ISocketSession session,
        IOperationMessagePayload connectionInitMessage,
        CancellationToken cancellationToken = new())
    {
        // Get the HttpContext for this WebSocket session
        var context = session.Connection.HttpContext;

        // We need the authentication scheme provider to determine the default scheme
        var provider = context.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>();

        // Convert the payload from the 'connection_init' into a dictionary to locate the token
        var properties = connectionInitMessage.As<Dictionary<string, string?>>();

        // If no payload is present, reject the connection
        if (properties is null)
        {
            return ConnectionStatus.Reject("Invalid connection init payload.");
        }

        // If there's a token under the 'authorization' key, we store it in HttpContext.Items.
        if (properties.TryGetValue(WEBOCKET_PAYLOAD_AUTH_KEY, out string? token) &&
            token is { } stringToken)
        {
            // The token might be prefixed with "Bearer ".
            // We remove that to only keep the raw token.
            context.Items[WEBSOCKET_TOKEN] = stringToken.StartsWith("Bearer ")
                ? stringToken["Bearer ".Length..]
                : stringToken;

            // Now we attempt to authenticate again, but this time we do NOT skip 
            // because the token is present. 
            // We retrieve the default (JWT) authentication scheme.
            var defaultAuthenticate = await provider.GetDefaultAuthenticateSchemeAsync();
            if (defaultAuthenticate is not null)
            {
                var result = await context.AuthenticateAsync(defaultAuthenticate.Name);

                // If authentication succeeds, set the HttpContext.User 
                // so that subsequent operations know which user this is.
                if (result.Principal is not null)
                {
                    context.User = result.Principal;
                    return ConnectionStatus.Accept();
                }
            }
        }

        // If there's no token or authentication fails, we reject the WebSocket connection.
        return ConnectionStatus.Reject();
    }
}