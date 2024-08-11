import React from "react";
import { ApolloProvider } from "@apollo/react-hooks";
import { Helmet } from "react-helmet";
import Router from "./configs/router";
import client from "./configs/apollo";

const App = () => (
  <ApolloProvider client={client}>
    <Helmet>
      <meta charSet='utf-8' />
      <title>Slack Clone</title>
    </Helmet>
    <Router />
  </ApolloProvider>
);

export default App;
