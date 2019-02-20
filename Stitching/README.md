# Schema Stitching Example

This example shows how you can implement as stitched schema with Hot Chocolate.

This example consists of the following projects:

- CustomerSchema
  The customer schema contains a GraphQL server that serves up a schema around a customer entity.

- ContractSchema
  The contract schema contains a GraphQL server that serves up a schema that provides insurance contract entities that can be assoicated with customers.

- Stitching
  The stitching project contains a GraphQL server that stitches the former mentiond GraphQL schemas together.

1. Start the customer and contract servers with `dotnet run`
2. When the former servers are running start the stitching server with `dotnet run`
3. Head over to `http://127.0.0.1/playground` and test out some queries.

[Hot Chocolate Documentation](https://hotchocolate.io)
