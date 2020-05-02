import styled from "styled-components";
import Background from "../../assets/auth-background.svg";

const AuthContainer = styled.div`
  background: GhostWhite;
  height: 100vh;
  background-image: url(${Background});
  background-repeat: no-repeat;
  background-size: cover;
`;

export default AuthContainer;
export { AuthContainer };
