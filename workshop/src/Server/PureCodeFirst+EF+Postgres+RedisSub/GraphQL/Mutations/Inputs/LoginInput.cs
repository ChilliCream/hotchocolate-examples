using HotChocolate;

namespace SlackClone.GraphQL.Mutations
{
    public class LoginInput
    {
        public LoginInput(
                    string email,
                    string password)
        {
            Email = email;
            Password = password;
        }
        [GraphQLNonNullType]
        public string Email { get; }
        [GraphQLNonNullType]
        public string Password { get; }
    }
}