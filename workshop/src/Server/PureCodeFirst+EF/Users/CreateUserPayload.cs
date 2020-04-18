namespace Chat.Server.Users
{
    public class CreateUserPayload
    {
        public CreateUserPayload(User user, string? clientMutationId) =>
            (User, ClientMutationId) = (user, clientMutationId);

        public User User { get; }

        public string? ClientMutationId { get; }
    }
}
