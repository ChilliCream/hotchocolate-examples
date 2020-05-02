import React, { useState } from "react";
import SidebarContainer from "../components/styled/SidebarContainer";
import ChannelList from "./ChannelList";
//import DirectMessageUsers from "./DirectMessageUsers";
import CreateChannelModal from "./CreateChannelModal";
//import DirectMessageModal from "./DirectMessageModal";
import SidebarHeader from "./SidebarHeader";
import User from "./User";

const Sidebar = ({ channelId }) => {
  const [isCreateChannelModalOpen, setCreateChannelModalIsOpen] = useState(false);
  //const [isDirectMessageModalOpen, setDirectMessageModalOpen] = useState(false);

  return (
    <SidebarContainer>
      <SidebarHeader />
      <ChannelList setIsOpen={setCreateChannelModalIsOpen} channelId={channelId} />
      <CreateChannelModal
        isOpen={isCreateChannelModalOpen}
        setIsOpen={setCreateChannelModalIsOpen}
      />
      {/*<DirectMessageUsers setIsOpen={setDirectMessageModalOpen} />
      <DirectMessageModal isOpen={isDirectMessageModalOpen} setIsOpen={setDirectMessageModalOpen} />*/}
      <User />
    </SidebarContainer>
  );
};

export default Sidebar;
