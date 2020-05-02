import React from "react";
import { Comment } from "semantic-ui-react";
import styled from "styled-components";
import { useQuery, useSubscription } from "@apollo/react-hooks";
import { CHANNEL_MESSAGES_GQL, ON_CHANNEL_MESSAGE_ADD_GQL } from "../gql/channels";
import { addToCache } from "../utils/graphqlCache";

const MessageListWrapper = styled.div`
  height: calc(100% - 120px);
  overflow-y: scroll;
`;

const MessageList = ({ channelId }) => {
  const { data, loading, error } = useQuery(CHANNEL_MESSAGES_GQL, {
    variables: {
      filter: { channelId },
    },
  });

  useSubscription(ON_CHANNEL_MESSAGE_ADD_GQL, {
    variables: {
      channelId,
    },
    onSubscriptionData: ({ client, subscriptionData }) =>
      addToCache(
        client,
        CHANNEL_MESSAGES_GQL,
        "channelMessages",
        subscriptionData,
        "onChannelMessageAdd",
        {
          filter: { channelId },
        }
      ),
  });

  if (loading) {
    return <div>loading...</div>;
  } else if (error) {
    return <div>error channel list</div>;
  }

  return (
    <MessageListWrapper>
      <Comment.Group>
        {data.channelMessages.map((i) => (
          <Comment key={i.id}>
            <Comment.Content>
              <Comment.Author as='a'>test</Comment.Author>
              <Comment.Metadata>
                <div>Today at 5:42PM</div>
              </Comment.Metadata>
              <Comment.Text>{i.text}</Comment.Text>
            </Comment.Content>
          </Comment>
        ))}
      </Comment.Group>
    </MessageListWrapper>
  );
};

export default MessageList;
