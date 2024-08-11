import React from "react";
import { Button, Header } from "semantic-ui-react";
import { Form, Field } from "react-final-form";
import * as yup from "yup";
import { useMutation } from "@apollo/react-hooks";
import { Link } from "react-router-dom";
import { useHistory } from "react-router-dom";

import CenteredContainer from "./styled/CenteredCard";
import { Textbox } from "./SemanticUIWrappers";
import { LOGIN_GQL } from "../gql/auth";
import AuthContainer from "./styled/AuthContainer";
import { validate } from "../utils/forms/validate";
const required = "- This field is required";

const validationSchema = yup.object({
  email: yup.string().email("- must be a valid email").required(required),
  password: yup.string().min(4, "- must be longer than four characters").required(required),
});

const Login = () => {
  const history = useHistory();
  const [Login] = useMutation(LOGIN_GQL);

  const onSubmit = async (values) => {
    const USER_NOT_EXIST = "The user does not exist";
    const INVALID_PASSWORD = "The specified password is invalid";
    Login({
      variables: {
        input: { email: values.email, password: values.password },
      },
    })
      .then(({ data }) => {
        localStorage.setItem("token", data.login.token);
        history.push("/channels/1252c325-7adf-4275-a448-fe877afe919c");
      })
      .catch((error) => {
        if (error.message.includes(USER_NOT_EXIST)) {
          alert(USER_NOT_EXIST);
        } else if (error.message.includes(INVALID_PASSWORD)) {
          alert(INVALID_PASSWORD);
        }
      });
  };

  return (
    <AuthContainer>
      <Form
        onSubmit={onSubmit}
        validate={(values) => validate(values, validationSchema)}
        render={({ handleSubmit, submitting, pristine }) => (
          <form onSubmit={handleSubmit}>
            <CenteredContainer height='320'>
              <Header textAlign='center' as='h2'>
                Login
              </Header>
              <Field name='email' text='EMAIL' component={Textbox} />
              <Field name='password' type='password' text='PASSWORD' component={Textbox} />
              <Button type='submit' color='violet' fluid disabled={submitting || pristine}>
                LOGIN
              </Button>
              <br />
              <div>
                Need an account? <Link to='/signup'>Signup</Link>
              </div>
            </CenteredContainer>
          </form>
        )}
      />
    </AuthContainer>
  );
};

export default Login;
