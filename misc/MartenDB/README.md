# [MartenDB](https://martendb.io/) Integration

## To run the project:
1. Update settings in `appsettins.json` file to point to an existing instance of Postgres, or bring up an instance of Postgres with docker as follows:
   ``` docker-compose up```
2. Run the project
3. Query using the GraphQl UI at `http://localhost:3000/graphql`


## Sample Filtering Queries

```
{
  # All the popular books
  books (where: {isPopular: {eq: true}})
  {
    id
    isPopular
    content
    rating {
      score
    }
  }
}
```

```
{
  # All the books with a rating score greater than 50
  books (where: {rating: {score: {gt: 50}}})
  {
    id
    isPopular
    content
    rating {
      score
    }
  }
}
```

```
{
  # All the books with content that contains the term 'cool'
  books (where: {content: {contains: "cool"}})
  {
    id
    isPopular
    content
    rating {
      score
    }
  }
}
```

```
{
  # All the popular books with content that contains the term 'cool'
  books (where: {and: [{content: {contains: "cool"}}, {isPopular: {eq: true}}]})
  {
    id
    isPopular
    content
    rating {
      score
    }
  }
}
```

## Sample Sorting Queries

```
{
  # All the popular books ordered by rating score (ascinding)
  books (where: {isPopular: {eq: true}}, order: {rating: {score: ASC}})
  {
    id
    isPopular
    content
    rating {
      score
    }
  }
}
```

```
{
  # All the popular books ordered by rating score (descinding)
  books (where: {isPopular: {eq: true}}, order: {rating: {score: DESC}})
  {
    id
    isPopular
    content
    rating {
      score
    }
  }
}
```

## And More!
You can do even more! See the docs for more details:
- [Fetching Data](https://chillicream.com/docs/hotchocolate/v13/fetching-data)
- [Marten-specific configuration](https://chillicream.com/docs/hotchocolate/v13/integrations/marten)
