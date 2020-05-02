namespace SlackClone.GraphQL.Mutations
{
    public class CreateMutationResponse<T> : MutationResponse
    {
        public T CreatedObject { get; set; }

        public CreateMutationResponse(
            bool ok,
            T createdObject = default) : base(ok)
        {
            CreatedObject = createdObject;
        }
    }
}
