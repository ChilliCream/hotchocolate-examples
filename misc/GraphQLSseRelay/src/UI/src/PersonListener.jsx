import React from "react";
import graphql from "babel-plugin-relay/macro";
import { useSubscription } from "react-relay";

// Define a query
export function PersonListener(props) {
  const [name, setName] = React.useState();
  useSubscription({
    onNext: ({ personAdded: { name } }) => {
      setName(name);
    },
    subscription: graphql`
      subscription PersonListener_PersonAdded_Subscription {
        personAdded {
          name
        }
      }
    `,
  });

  return <div>Recently added: {name}</div>;
}
