import fs from 'fs';
import { ApolloServer } from '@apollo/server';
import { startStandaloneServer } from '@apollo/server/standalone';

// A schema is a collection of type definitions (hence "typeDefs")
// that together define the "shape" of queries that are executed against
// your data.
const typeDefs = fs.readFileSync('./src/schema.graphql', 'utf8');

const users = [
    {
      id: 1,
      name: 'Ada Lovelace',
      birthdate: '1812-12-10',
      username: '@ada',
    },
    {
      id: 2,
      name: 'Alan Turing',
      birthdate: '1912-06-23',
      username: '@alan',
    },
  ];

// Resolvers define how to fetch the types defined in your schema.
// This resolver retrieves books from the "books" array above.
const resolvers = {
    Query: {
      users: () => users,
      userById: (parent, args, context, info) => {
        return users.find(user => user.id === args.id);
      },
      usersById: (parent, args, context, info) => {
        return users.filter(user => args.ids.includes(user.id));
      },
      userByUsername: (parent, args, context, info) => {
        return users.find(user => user.username === args.username);
      }
    },
  };

// The ApolloServer constructor requires two parameters: your schema
// definition and your set of resolvers.
const server = new ApolloServer({
  typeDefs,
  resolvers,
});

// Passing an ApolloServer instance to the `startStandaloneServer` function:
//  1. creates an Express app
//  2. installs your ApolloServer instance as middleware
//  3. prepares your app to handle incoming requests
const { url } = await startStandaloneServer(server, {
  listen: { port: 4000 },
});

console.log(`🚀  Server ready at: ${url}`);