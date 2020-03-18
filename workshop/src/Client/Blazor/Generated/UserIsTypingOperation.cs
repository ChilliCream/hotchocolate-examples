using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class UserIsTypingOperation
        : IOperation<IUserIsTyping>
    {
        public string Name => "userIsTyping";

        public IDocument Document => Queries.Default;

        public OperationKind Kind => OperationKind.Mutation;

        public Type ResultType => typeof(IUserIsTyping);

        public Optional<string> WritingTo { get; set; }

        public IReadOnlyList<VariableValue> GetVariableValues()
        {
            var variables = new List<VariableValue>();

            if (WritingTo.HasValue)
            {
                variables.Add(new VariableValue("writingTo", "String", WritingTo.Value));
            }

            return variables;
        }
    }
}
