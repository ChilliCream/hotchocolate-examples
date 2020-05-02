import React from "react";
import { Menu, Icon, Grid } from "semantic-ui-react";
import styled from "styled-components";
import { useQuery, useSubscription } from "@apollo/react-hooks";
import { useHistory } from "react-router-dom";

import SidebarHeaderText from "./styled/SidebarHeaderText";
import { CHANNELS_GQL, ON_CREATE_CHANNEL_GQL } from "../gql/channels";
import { addToCache } from "../utils/graphqlCache";

const StyledMenu = styled(Menu)`
  padding-left: 15px;
  padding-right: 8px;
`;

const ChannelList = ({ setIsOpen, channelId }) => {
  const history = useHistory();

  const { data, loading, error } = useQuery(CHANNELS_GQL);

  useSubscription(ON_CREATE_CHANNEL_GQL, {
    onSubscriptionData: ({ client, subscriptionData }) =>
      addToCache(client, CHANNELS_GQL, "channels", subscriptionData, "onCreateChannel"),
  });

  if (loading) {
    return <div>loading...</div>;
  } else if (error) {
    return <div>error channel list</div>;
  }

  const handleChannelChange = (newChannelId) => {
    history.push(`/channels/${newChannelId}`);
  };

  return (
    <div>
      <Grid columns={2}>
        <Grid.Column width={12}>
          <SidebarHeaderText>CHANNELS</SidebarHeaderText>
        </Grid.Column>
        <Grid.Column width={2}>
          <Icon inverted name='plus' link onClick={() => setIsOpen(true)} />
        </Grid.Column>
      </Grid>
      <StyledMenu inverted fluid secondary vertical>
        {data.channels.map((i) => (
          <Menu.Item
            key={i.id}
            name={i.name}
            content={`# ${i.name}`}
            active={channelId === i.id}
            onClick={() => handleChannelChange(i.id)}
          />
        ))}
      </StyledMenu>
    </div>
  );
};
export default ChannelList;
