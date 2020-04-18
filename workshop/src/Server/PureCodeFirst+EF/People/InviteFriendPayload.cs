using System.Security.Cryptography;

namespace Chat.Server.People
{
    public class InviteFriendPayload
    {
        public InviteFriendPayload(Person me, string? clientMutationId) =>
            (Me, ClientMutationId) = (me, clientMutationId);

        public Person Me { get; }

        public string? ClientMutationId { get; }
    }
}
