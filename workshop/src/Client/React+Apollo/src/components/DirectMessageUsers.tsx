import React from "react";
import { Grid, Icon } from "semantic-ui-react";
import styled from "styled-components";

const DirectMessageUsersWrapper = styled.ul`
  list-style-type: none;
  margin: 15;
  padding: 0;
  width: 100%;
  background-color: grey;
`;

const DirectMessageUser = styled.li`
  li a {
    display: block;
    color: #000;
    padding: 8px 16px;
    text-decoration: none;
  }
  li a:hover {
    background-color: #555;
    color: white;
    cursor: pointer;
  }
`;

const SidebarHeaderText = styled.span`
  padding: 15px;
  font-size: 12px;
  font-weight: 700;
  color: white;
`;

const DirectMessageUsers = ({ setIsOpen }) => (
  <div>
    <Grid columns={2}>
      <Grid.Column width={12}>
        <SidebarHeaderText>DIRECT MESSAGES</SidebarHeaderText>
      </Grid.Column>
      <Grid.Column textAlign='right' width={2}>
        <Icon inverted name='plus' link onClick={() => setIsOpen(true)} />
      </Grid.Column>
    </Grid>
    <DirectMessageUsersWrapper>
      <DirectMessageUser>hello</DirectMessageUser>
      <DirectMessageUser>test</DirectMessageUser>
    </DirectMessageUsersWrapper>
  </div>
);

export default DirectMessageUsers;
