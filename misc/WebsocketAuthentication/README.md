# JWT Over WebSockets with ASP.NET & GraphQL

This repository demonstrates how to handle **JWT-based authentication** for **WebSocket** connections in an ASP.NET application using [HotChocolate](https://github.com/ChilliCream/hotchocolate)

It covers:

- **Why** WebSockets pose a challenge for authentication
- **How** to create a "stub" authentication scheme to allow the HTTP handshake to upgrade without failing
- **How** to intercept the WebSocket `connection_init` message to provide the JWT token
- **How** the application can handle normal HTTP and WebSocket requests seamlessly

---

## Contents

1. [The Challenge](#the-challenge)
2. [Conceptual Overview](#conceptual-overview)
3. [Project Structure](#project-structure)
4. [Getting Started](#getting-started)
5. [Testing the Flow](#testing-the-flow)
6. [Security Considerations](#security-considerations)

---

## The Challenge

When a GraphQL WebSocket connection is established, it begins as an **HTTP Upgrade** request. Traditional JWT validation is designed to look for tokens in headers (e.g., the `Authorization` header). However:

- **Before** the connection becomes a WebSocket, there's exactly one HTTP request to “upgrade” the connection.
- **During** this handshake, no additional headers can be sent (beyond the standard `Upgrade` header) (at least in the Browser WebSocket API).
- ASP.NET's authentication system **caches** the authentication result for the duration of the request.

Therefore, if the server tries to validate a token at this point and can’t find it in the normal header location (which might not be settable during an Upgrade), it **fails** or remains unauthenticated.

---

## Conceptual Overview

1. **Add a Stub Authentication Scheme** (`WebsocketScheme`):

   - This scheme explicitly **does not** attempt to authenticate.
   - If the request is recognized as a WebSocket upgrade, we forward it to this stub, effectively skipping normal JWT validation at handshake time.

2. **Intercept the WebSocket’s `connection_init` Message**:

   - Once the WebSocket connection is established, the client can send a token via the `connection_init` message.
   - We capture the token, store it in the `HttpContext` (so it can be used by ASP.NET’s authentication system), and re-trigger authentication.

3. **JWT Bearer Scheme with Forwarder**:

   - The default authentication scheme checks whether the request is a valid WebSocket upgrade without a token. If so, it forwards to the stub scheme. Otherwise, it uses the real JWT Bearer scheme.
   - For the second authentication pass (once we have a token in `HttpContext`), it can validate that token as if it were present in a header.

4. **Result**:
   - **HTTP requests** are authenticated via normal JWT Bearer.
   - **WebSocket upgrade requests** initially bypass JWT validation but then get authenticated manually as soon as the `connection_init` message provides a valid token.

---

## Getting Started

### Prerequisites

- [.NET 9 or later](https://dotnet.microsoft.com/en-us/download)
- Basic knowledge of GraphQL and HotChocolate (optional but helpful)

### Installation & Run

1. **Clone** or **download** this repository.
2. Open a terminal in the project’s directory.
3. Run the project:
   ```bash
   dotnet run
   ```
4. The application will start on the configured port (e.g., `http://localhost:5095/graphql`).

### Fetching a Token (Demo)

A simple endpoint is provided to fetch a token for demo purposes from the [Duende IdentityServer Demo](https://demo.duendesoftware.com).

You can get one by browsing to:

```
GET http://localhost:5095/token
```

This returns a JSON response with an `access_token`.

---

## Testing the Flow

1. Open Nitro on https://localhost:5095/graphql
2. Get an access token from the `/token` endpoint.
3. Configure Nitro to use the token:
   - Click the **Settings** icon in the top right.
   - Go to the Authentication tab.
   - Choose **Bearer Token** and paste the token.
   - Nitro will forward this header as the connection_init parameter "authorization".
4. Execute the subscription query:
   ```graphql
   subscription {
     onTimedEvent {
       count
       isAuthenticated
     }
   }
   ```

---

## Security Considerations

- **Long-lived WebSockets**: Once the WebSocket is established, you have a potentially long session. Tokens used this way **will not refresh automatically**. You may need to enforce expiry logic or periodically close/reestablish connections if security tokens expire.
