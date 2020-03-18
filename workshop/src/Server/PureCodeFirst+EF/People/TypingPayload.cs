namespace Chat.Server.People
{
    public class TypingPayload
    {
        public TypingPayload(Person recipient, Person sender, string? clientMutationId)
        {
            Recipient = recipient;
            Sender = sender;
            ClientMutationId = clientMutationId;
        }

        /// <summary>
        /// The person to which a message is being typed.
        /// </summary>
        public Person Recipient { get; }

        /// <summary>
        /// The email of the person who is typing the message.
        /// </summary>
        public Person Sender { get; }

        /// <summary>
        /// The client mutation id which can be optionally provided with a mutation.
        /// </summary>
        public string? ClientMutationId { get; }
    }
}