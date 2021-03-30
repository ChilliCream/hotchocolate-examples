# MongoDB Filtering Example

This is a example that shows how you can integrate the Date API with MongoDB.

To run this example you will need to have a instance of MongoDB running locally.

You can use docker to start one:
```
docker run --name -d mongo mongo:latest
```

To add example data to you project you can go to http://localhost:5000/graphql and run the following mutation:
```graphql
mutation {
  a: createPerson(
    input: {
      name: "Yorker Shorton"
      mainAddress: { city: "Denver", street: "04 Leroy Trail" }
      addresses: { city: "Denver", street: "04 Leroy Trail" }
    }
  ) {
    person {
      addresses {
        city
        street
      }
      mainAddress {
        city
        street
      }
      id
      name
    }
  }
  b: createPerson(
    input: {
      name: "Mellie Blunsden"
      mainAddress: { city: "Villaba", street: "5121 Riverside Parkway" }
      addresses: { city: "Villaba", street: "5121 Riverside Parkway" }
    }
  ) {
    person {
      addresses {
        city
        street
      }
      mainAddress {
        city
        street
      }
      id
      name
    }
  }
  c: createPerson(
    input: {
      name: "Romeo McCloy"
      mainAddress: { city: "Orion", street: "2 Sunfield Plaza" }
      addresses: { city: "Orion", street: "2 Sunfield Plaza" }
    }
  ) {
    person {
      addresses {
        city
        street
      }
      mainAddress {
        city
        street
      }
      id
      name
    }
  }
}

```
