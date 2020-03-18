namespace Chat.Server.Users
{
    public class LoginInput
    {
        public LoginInput(
            string email,
            string password,
            string? clientMutationId)
        {
            Email = email;
            Password = password;
            ClientMutationId = clientMutationId;
        }

        public string Email { get; }

        public string Password { get; }

        public string? ClientMutationId { get; }
    }
}
