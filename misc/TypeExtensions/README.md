![ChilliCream](https://github.com/ChilliCream/graphql-workshop/raw/master/docs/images/ChilliCream.svg)

# Add Type Extension Example

Type extensions allow us to add, remove or replace fields on existing types, without necessarily needing access to these types.

[GitHub Repo](https://github.com/ChilliCream/hotchocolate-examples/tree/master/misc/TypeExtensions)

&nbsp;

## Learn
In this [YouTube Learn Video](https://www.youtube.com/watch?v=EHTr4Fq6GlA), we will look at the Hot Chocolate type extension feature. We will explore how we can reshape schema types with the annotation-based, code-first, and schema-first approaches without having to bleed GraphQL type specifics into the business layer.

In the [Extending Types Documentation](https://chillicream.com/docs/hotchocolate/defining-a-schema/extending-types) you will find more information about the type extension feature.

&nbsp;

## Practice
At first, rund `dotnet watch` in the app directory. Then you can open the [Banana Cake Pop IDE](https://localhost:5001/graphql/) in your browser and run the following query:
```javascript
{
  book {
    id
    author {
      birthdate
    }
  }
}
```
For more information about `dotnet watch`, see the [dotnet watch documentation](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-watch).

&nbsp;

## Additional resources
[Banana Cake Pop Documentation](https://chillicream.com/docs/bananacakepop)

[ChilliCream GraphQL Workshop](https://github.com/ChilliCream/graphql-workshop)

[GraphQL API in .NET w/ Hot Chocolate](https://www.youtube.com/playlist?list=PLA8ZIAm2I03g9z705U3KWJjTv0Nccw9pj)

[GraphQL Client in .NET w/ Strawberry Shake](https://www.youtube.com/playlist?list=PLA8ZIAm2I03hQoVCdRzADYN6UBLnJNaSl)

&nbsp;

## About
[<img src="https://twitter.com/favicon.ico" height="15" style="margin-bottom: -3px" /> Twitter](https://twitter.com/Chilli_Cream)

[<img src="https://www.youtube.com/favicon.ico" height="15" style="margin-bottom: -3px" /> YouTube](https://www.youtube.com/c/ChilliCream)

[<img src="https://github.com/favicon.ico" height="15" style="margin-bottom: -3px" /> GitHub](https://github.com/ChilliCream)

[<img src="https://reddit.com/favicon.ico" height="15" style="margin-bottom: -3px" /> Reddit](https://www.reddit.com/user/michael_staib/)