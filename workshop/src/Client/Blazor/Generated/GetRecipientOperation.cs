using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class GetRecipientOperation
        : IOperation<IRecipientById>
    {
        public string Name => "getRecipient";

        public IDocument Document => Queries.Default;

        public OperationKind Kind => OperationKind.Query;

        public Type ResultType => typeof(IRecipientById);

        public Optional<string> RecipientId { get; set; }

        public IReadOnlyList<VariableValue> GetVariableValues()
        {
            var variables = new List<VariableValue>();

            if (RecipientId.HasValue)
            {
                variables.Add(new VariableValue("recipientId", "ID", RecipientId.Value));
            }

            return variables;
        }
    }
}
