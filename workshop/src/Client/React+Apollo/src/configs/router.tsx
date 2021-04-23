import React from "react";
import { BrowserRouter, Switch, Route, Redirect } from "react-router-dom";
import Signup from "../components/Signup";
import Login from "../components/Login";
//import Footer from "../components/Footer";
import Layout from "../components/Layout";

const isAuthenticated = () => {
  const token = localStorage.getItem("token");

  if (token !== null) {
    return true;
  }
  return false;
};

const PrivateRoute = ({ children, ...rest }) => {
  return (
    <Route
      {...rest}
      render={({ location }) =>
        isAuthenticated() ? (
          children
        ) : (
          <Redirect
            to={{
              pathname: "/login",
              state: { from: location },
            }}
          />
        )
      }
    />
  );
};

const Router = () => (
  <BrowserRouter>
    <Switch>
      <PrivateRoute path='/' exact>
        <Layout />
      </PrivateRoute>
      <Route path='/signup' exact component={Signup} />
      <Route path='/login' exact component={Login} />
      <Route path='/channels/:id' component={Layout} />
    </Switch>
  </BrowserRouter>
);

export default Router;
