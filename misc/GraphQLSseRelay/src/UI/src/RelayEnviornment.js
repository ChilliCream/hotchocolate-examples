import {
  Observable,
  Environment,
  Network,
  RecordSource,
  Store,
} from "relay-runtime";

import { createClient } from "graphql-sse";

const subscriptionsClient = createClient({
  url: "https://localhost:5001/graphql",
  headers: {
    "content-type": "application/json",
  },
});

// yes, both fetch AND subscribe can be handled in one implementation
function fetchOrSubscribe(operation, variables) {
  return Observable.create((sink) => {
    if (!operation.text) {
      return sink.error(new Error("Operation text cannot be empty"));
    }
    return subscriptionsClient.subscribe(
      {
        operationName: operation.name,
        query: operation.text,
        variables,
      },
      sink
    );
  });
}

export const network = Network.create(fetchOrSubscribe, fetchOrSubscribe);

// Export a singleton instance of Relay Environment configured with our network function:
export default new Environment({
  network: network,
  store: new Store(new RecordSource()),
});
