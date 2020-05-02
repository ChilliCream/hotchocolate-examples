using HotChocolate;

namespace SlackClone.GraphQL.Mutations
{
    public class SignupInput
    {
        public SignupInput(
            string displayName,
            string email,
            string password)
        {
            DisplayName = displayName;
            Email = email;
            Password = password;
        }

        [GraphQLNonNullType]
        public string DisplayName { get; }
        [GraphQLNonNullType]
        public string Email { get; }
        [GraphQLNonNullType]
        public string Password { get; }
    }
}