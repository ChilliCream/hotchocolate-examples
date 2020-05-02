import React from "react";
import { Divider } from "semantic-ui-react";
import { useQuery } from "@apollo/react-hooks";
import { CHANNELS_GQL } from "../gql/channels";

const ChannelHeader = ({ channelId }) => {
  const { data, loading, error } = useQuery(CHANNELS_GQL, {
    variables: {
      filter: { id: channelId },
    },
  });

  if (loading) {
    return <div>loading...</div>;
  } else if (error) {
    return <div>error channel list</div>;
  }

  return (
    <div>
      <div>
        <strong># {data.channels[0].name}</strong>
      </div>
      {/*<Icon name='user outline' />
      45 */}
      <Divider />
    </div>
  );
};

export default ChannelHeader;
