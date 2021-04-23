using System;
namespace SlackClone.GraphQL.Mutations
{
    public class AddMessageToChannelResponse
    {
        public bool Ok { get; }

        public AddMessageToChannelResponse(bool ok)
        {
            Ok = ok;
        }
    }
}
