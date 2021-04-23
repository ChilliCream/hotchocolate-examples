import gql from "graphql-tag";

export const SIGNUP_GQL = gql`
  mutation SIGNUP($input: SignupInput) {
    signUp(input: $input) {
      ok
    }
  }
`;

export const LOGIN_GQL = gql`
  mutation LOGIN($input: LoginInput) {
    login(input: $input) {
      token
    }
  }
`;
