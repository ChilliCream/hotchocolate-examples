import React from "react";
import { Button, Modal } from "semantic-ui-react";

const DirectMessageModal = ({ isOpen, setIsOpen }) => (
  <Modal
    closeOnDimmerClick={false}
    size='tiny'
    closeIcon
    open={isOpen}
    onClose={() => setIsOpen(false)}
  >
    <Modal.Header as='h1'>Direct Messages</Modal.Header>
    <Modal.Content></Modal.Content>
    <Modal.Actions>
      <Button color='green'>Chat</Button>
    </Modal.Actions>
  </Modal>
);

export default DirectMessageModal;
