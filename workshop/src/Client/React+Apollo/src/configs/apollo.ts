import { HttpLink } from "apollo-link-http";
import { WebSocketLink } from "apollo-link-ws";
import { getMainDefinition } from "apollo-utilities";
import { ApolloClient } from "apollo-client";
import { InMemoryCache } from "apollo-cache-inmemory";
import { ApolloLink, concat, split } from "apollo-link";

// Create an http link:
const httpLink = new HttpLink({
  uri: "https://slackclone-server.herokuapp.com/",
});

const authMiddleware = new ApolloLink((operation, forward) => {
  // add the authorization to the headers
  operation.setContext({
    headers: {
      authorization: `Bearer ${localStorage.getItem("token") || null}`,
    },
  });

  return forward(operation);
});

// Create a WebSocket link:
const wsLink = new WebSocketLink({
  uri: "wss://slackclone-server.herokuapp.com/",
  options: {
    reconnect: true,
  },
});

// using the ability to split links, you can send data to each link
// depending on what kind of operation is being sent
const link = split(
  // split based on operation type
  ({ query }) => {
    const definition = getMainDefinition(query);
    return definition.kind === "OperationDefinition" && definition.operation === "subscription";
  },
  wsLink,
  concat(authMiddleware, httpLink)
);

export const client = new ApolloClient({
  cache: new InMemoryCache(),
  link,
});

export default client;
