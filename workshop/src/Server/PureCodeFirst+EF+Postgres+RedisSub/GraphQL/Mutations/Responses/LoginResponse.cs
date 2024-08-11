using SlackClone.Models;

namespace SlackClone.GraphQL.Mutations
{
    public class LoginResponse
    {
        public LoginResponse(
            User me,
            string token)
        {
            Me = me;
            Token = token;
        }
        public User Me { get; }
        public string Token { get; }
    }
}