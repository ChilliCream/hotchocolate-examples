using System;

namespace SlackClone.GraphQL.Mutations
{
    public class AddMessageToChannelInput
    {
        /// <summary>
        /// The id of the channel to which a message shall be added.
        /// </summary>
        public Guid ChannelId { get; }

        /// <summary>
        /// The message text.
        /// </summary>
        public string Text { get; }

        public AddMessageToChannelInput(
            Guid channelId,
            string text)
        {
            ChannelId = channelId;
            Text = text;
        }
    }
}
