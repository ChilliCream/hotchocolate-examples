import React from "react";
import { Modal, Button } from "semantic-ui-react";
import { Form, Field } from "react-final-form";
import { useMutation } from "@apollo/react-hooks";

import { Textbox } from "./SemanticUIWrappers";
import { CREATE_CHANNEL_GQL } from "../gql/channels";

const CreateChannelModal = ({ isOpen, setIsOpen }) => {
  const [CreateChannel] = useMutation(CREATE_CHANNEL_GQL);

  const onSubmit = async (values) => {
    CreateChannel({
      variables: {
        input: values,
      },
    }).then(({ data }) => {
      if (data.createChannel.ok) {
        setIsOpen(false);
      }
    });
  };
  return (
    <Modal
      closeOnDimmerClick={false}
      size='tiny'
      closeIcon
      open={isOpen}
      onClose={() => setIsOpen(false)}
    >
      <Modal.Header as='h1'>Create a channel</Modal.Header>
      <Modal.Content>
        <Modal.Description>
          Channels are where your team communicates. They’re best when organized around a topic —
          #marketing, for example.
        </Modal.Description>
        <Form
          onSubmit={onSubmit}
          render={({ handleSubmit, submitting, pristine }) => (
            <form onSubmit={handleSubmit}>
              <Field name='name' text='Name' component={Textbox} />
              <Field name='description' text='Description' component={Textbox} />

              <Button type='submit' color='green'>
                Create
              </Button>
            </form>
          )}
        />
      </Modal.Content>
    </Modal>
  );
};

export default CreateChannelModal;
