using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class GetPeopleAndRecipientOperation
        : IOperation<IGetPeopleAndRecipient>
    {
        public string Name => "getPeopleAndRecipient";

        public IDocument Document => Queries.Default;

        public OperationKind Kind => OperationKind.Query;

        public Type ResultType => typeof(IGetPeopleAndRecipient);

        public Optional<string> UserId { get; set; }

        public Optional<string> RecipientId { get; set; }

        public IReadOnlyList<VariableValue> GetVariableValues()
        {
            var variables = new List<VariableValue>();

            if (UserId.HasValue)
            {
                variables.Add(new VariableValue("userId", "ID", UserId.Value));
            }

            if (RecipientId.HasValue)
            {
                variables.Add(new VariableValue("recipientId", "ID", RecipientId.Value));
            }

            return variables;
        }
    }
}
