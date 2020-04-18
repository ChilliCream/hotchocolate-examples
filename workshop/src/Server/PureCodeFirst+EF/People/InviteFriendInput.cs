namespace Chat.Server.People
{
    public class InviteFriendInput
    {
        public InviteFriendInput(string email, string? clientMutationId) =>
            (Email, ClientMutationId) = (email, clientMutationId);

        public string Email { get; }

        public string? ClientMutationId { get; }
    }
}
