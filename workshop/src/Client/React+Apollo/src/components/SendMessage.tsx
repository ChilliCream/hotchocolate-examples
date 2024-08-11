import React from "react";
import { Input } from "semantic-ui-react";
import styled from "styled-components";
import { useMutation } from "@apollo/react-hooks";
import { ADD_MESSAGE_TO_CHANNEL_GQL } from "../gql/channels";

const SendMessageWrapper = styled.div`
  padding: 8px;
  position: absolute;
  left: 0;
  bottom: 0;
  width: 100%;
  height: 60px;
  background: GhostWhite;
`;

const SendMessage = ({ channelId }) => {
  const [AddMessageToChannel] = useMutation(ADD_MESSAGE_TO_CHANNEL_GQL);

  const handleAddMessage = (text) => {
    if (text !== null) {
      AddMessageToChannel({
        variables: {
          input: {
            channelId,
            text,
          },
        },
      });
    }
  };
  const ENTER_KEY = 13;

  return (
    <SendMessageWrapper>
      <Input
        onKeyDown={(e) => {
          if (e.keyCode === ENTER_KEY) {
            handleAddMessage(e.target.value);
          }
        }}
        focus
        fluid
        action={{ icon: "send" }}
        placeholder='Message...'
      />
    </SendMessageWrapper>
  );
};

export default SendMessage;
