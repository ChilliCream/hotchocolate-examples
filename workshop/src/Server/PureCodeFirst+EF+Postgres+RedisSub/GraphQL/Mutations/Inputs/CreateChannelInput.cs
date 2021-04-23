using System;
namespace SlackClone.GraphQL.Mutations
{
    public class CreateChannelInput
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public CreateChannelInput(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
