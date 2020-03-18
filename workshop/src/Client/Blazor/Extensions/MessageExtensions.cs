namespace Client.Extensions
{
    public static class MessageExtensions
    {
        public static string GetDirection(this IMessage message)
        {
            return message.Direction == Direction.Incoming
                ? "incoming"
                : "outgoing";
        }
    }
}