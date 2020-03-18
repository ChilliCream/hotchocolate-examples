using Chat.Server.People;

namespace Chat.Server.Users
{
    public class LoginPayload
    {
        public LoginPayload(
            Person me,
            string token,
            string scheme,
            string? clientMutationId)
        {
            Me = me;
            Token = token;
            Scheme = scheme;
            ClientMutationId = clientMutationId;
        }

        public Person Me { get; }

        public string Token { get; }

        public string Scheme { get; }

        public string? ClientMutationId { get; }
    }
}
