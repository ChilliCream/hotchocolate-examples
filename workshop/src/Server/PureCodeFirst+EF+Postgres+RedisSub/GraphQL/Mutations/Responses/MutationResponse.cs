using System.Collections.Generic;

namespace SlackClone.GraphQL.Mutations
{
    public class MutationResponse : IMutationResponse
    {
        public bool Ok { get; }

        public MutationResponse(bool ok)
        {
            Ok = ok;
        }
    }
}
