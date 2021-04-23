import styled from "styled-components";

const CenteredCard = styled.div`
  background: white;
  border: 1px solid rgba(0, 0, 0, 0.3);
  border-radius: 10px;
  box-shadow: 0 0 16px rgba(0, 0, 0, 0.3);
  position: fixed;
  z-index: 999;
  height: ${(props) => (props.height ? `${props.height}px` : "400px")};
  width: 500px;
  margin: auto;
  padding: 20px;
  top: 0;
  left: 0;
  bottom: 0;
  right: 0;
`;

export default CenteredCard;
