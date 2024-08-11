import gql from "graphql-tag";

export const CHANNELS_GQL = gql`
  query CHANNELS($filter: ChannelFilter, $sort: ChannelSort) {
    channels(where: $filter, order_by: $sort) {
      id
      name
    }
  }
`;

export const CHANNEL_MESSAGES_GQL = gql`
  query CHANNEL_MESSAGES($filter: ChannelMessageFilter, $sort: ChannelMessageSort) {
    channelMessages(where: $filter, order_by: $sort) {
      id
      text
      createdAtUTC
    }
  }
`;

export const ADD_MESSAGE_TO_CHANNEL_GQL = gql`
  mutation ADD_MESSAGE_TO_CHANNEL($input: AddMessageToChannelInput) {
    addMessageToChannel(input: $input) {
      ok
    }
  }
`;

export const ON_CHANNEL_MESSAGE_ADD_GQL = gql`
  subscription ON_CHANNEL_MESSAGE_ADD($channelId: Uuid!) {
    onChannelMessageAdd(channelId: $channelId) {
      id
      text
      createdAtUTC
    }
  }
`;

export const CREATE_CHANNEL_GQL = gql`
  mutation CREATE_CHANNEL($input: CreateChannelInput) {
    createChannel(input: $input) {
      ok
    }
  }
`;

export const ON_CREATE_CHANNEL_GQL = gql`
  subscription ON_CREATE_CHANNEL {
    onCreateChannel {
      id
      name
    }
  }
`;
