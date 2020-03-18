namespace Client.Extensions
{
    public static class ParticipantExtensions
    {
        public static string GetStatus(this IParticipant participant)
        {
            return participant.IsOnline == true
                ? "online"
                : "offline";
        }
    }
}