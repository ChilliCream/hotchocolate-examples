namespace Chat.Server.People
{
    public class InviteFriendPayload
    {
        public InviteFriendPayload(Person me, string? clientMutationId)
        {
            Me = me;
            ClientMutationId = clientMutationId;
        }

        public Person Me { get; }

        public string? ClientMutationId { get; }
    }
}
