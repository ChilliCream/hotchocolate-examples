using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class SendMessageOperation
        : IOperation<ISendMessage>
    {
        public string Name => "sendMessage";

        public IDocument Document => Queries.Default;

        public OperationKind Kind => OperationKind.Mutation;

        public Type ResultType => typeof(ISendMessage);

        public Optional<string> RecipientEmail { get; set; }

        public Optional<string> Text { get; set; }

        public IReadOnlyList<VariableValue> GetVariableValues()
        {
            var variables = new List<VariableValue>();

            if (RecipientEmail.HasValue)
            {
                variables.Add(new VariableValue("recipientEmail", "String", RecipientEmail.Value));
            }

            if (Text.HasValue)
            {
                variables.Add(new VariableValue("text", "String", Text.Value));
            }

            return variables;
        }
    }
}
