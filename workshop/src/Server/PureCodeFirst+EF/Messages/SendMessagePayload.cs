using Chat.Server.People;

namespace Chat.Server.Messages
{
    public class SendMessagePayload
    {
        public SendMessagePayload(
            Person sender,
            Person recipient,
            Message message,
            string? clientMutationId)
        {
            Sender = sender;
            Recipient = recipient;
            Message = message;
            ClientMutationId = clientMutationId;
        }

        public Person Sender { get; }

        public Person Recipient { get; }

        public Message Message { get; }

        public string? ClientMutationId { get; }
    }
}
