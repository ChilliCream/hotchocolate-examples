import React from "react";
import styled from "styled-components";
import { Button } from "semantic-ui-react";
import { useHistory } from "react-router-dom";
const UserContainer = styled.div`
  position: fixed;
  bottom: 0px;
  padding: 15px;
  width: 250px;
`;

const User = () => {
  const history = useHistory();

  const logout = () => {
    localStorage.removeItem("token");
    history.push("/login");
  };

  return (
    <UserContainer>
      <Button fluid onClick={() => logout()}>
        Logout
      </Button>
    </UserContainer>
  );
};

export default User;
