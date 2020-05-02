import React from "react";
import Container from "./styled/Container";
import Sidebar from "./Sidebar";
import MessageList from "./MessageList";
import SendMessage from "./SendMessage";
import ChannelHeader from "./ChannelHeader";
import styled from "styled-components";
import { useParams } from "react-router-dom";

const MessagesContainer = styled.div`
  padding: 15px;
  margin-left: 250px;
  position: absolute;
  color: #958993;
  height: 100vh;
  width: calc(100% - 250px);
`;

const Layout = () => {
  let { id } = useParams();

  return (
    <Container>
      <Sidebar channelId={id} />
      <MessagesContainer>
        <ChannelHeader channelId={id} />
        <MessageList channelId={id} />
        <SendMessage channelId={id} />
      </MessagesContainer>
    </Container>
  );
};

export default Layout;
