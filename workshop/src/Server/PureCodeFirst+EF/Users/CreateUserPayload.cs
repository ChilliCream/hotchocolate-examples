namespace Chat.Server.Users
{
    public class CreateUserPayload
    {
        public CreateUserPayload(User user, string? clientMutationId)
        {
            User = user;
            ClientMutationId = clientMutationId;
        }

        public User User { get; }

        public string? ClientMutationId { get; }
    }
}
