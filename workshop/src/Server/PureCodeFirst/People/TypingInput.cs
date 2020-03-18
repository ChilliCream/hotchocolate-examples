namespace Chat.Server.People
{
    public class TypingInput
    {
        public TypingInput(string writingTo, string? clientMutationId)
        {
            WritingTo = writingTo;
            ClientMutationId = clientMutationId;
        }

        /// <summary>
        /// The email of the person to which a message is being typed.
        /// </summary>
        public string WritingTo { get; }

        /// <summary>
        /// The client mutation id which can be optionally provided with a mutation.
        /// </summary>
        public string? ClientMutationId { get; }
    }
}