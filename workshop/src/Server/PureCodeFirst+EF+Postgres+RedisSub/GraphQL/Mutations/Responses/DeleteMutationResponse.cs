using System;
namespace SlackClone.GraphQL.Mutations.Responses
{
    public class DeleteMutationResponse
    {
        public bool Ok { get; }
        public Guid DeletedId { get; set; }

        public DeleteMutationResponse(bool ok, Guid deletedId)
        {
            Ok = ok;
            DeletedId = deletedId;
        }
    }
}
