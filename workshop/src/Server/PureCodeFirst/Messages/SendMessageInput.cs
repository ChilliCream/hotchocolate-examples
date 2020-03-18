using System;

namespace Chat.Server.Messages
{
    public class SendMessageInput
    {
        public SendMessageInput(
            string recipientEmail,
            string text,
            string? clientMutationId)
        {
            RecipientEmail = recipientEmail;
            Text = text;
            ClientMutationId = clientMutationId;
        }

        /// <summary>
        /// The email of the person to which a message shall be send.
        /// </summary>
        public string RecipientEmail { get; }

        /// <summary>
        /// The message text.
        /// </summary>
        public string Text { get; }

        public string? ClientMutationId { get; }
    }
}
