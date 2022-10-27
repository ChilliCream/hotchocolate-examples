import React from "react";
import graphql from "babel-plugin-relay/macro";
import { useMutation } from "react-relay";

// Define a query
export function PersonForm(props) {
  const [name, setName] = React.useState();
  const [mutate, isInFlight] = useMutation(graphql`
    mutation PersonForm_CreatePerson_Mutation($input: AddPersonInput!) {
      addPerson(input: $input) {
        person {
          name
        }
      }
    }
  `);

  const handleSubmit = React.useCallback(
    (e) => {
      e.stopPropagation();
      e.preventDefault();
      mutate({
        variables: { input: { person: { name } } },
        onCompleted: () => {
          setName("");
        },
      });
    },
    [mutate, name]
  );

  return (
    <form>
      <input
        type="text"
        value={name}
        onChange={(e) => setName(e.target.value)}
      />
      <button disabled={isInFlight} onClick={handleSubmit}>
        Add user
      </button>
    </form>
  );
}
