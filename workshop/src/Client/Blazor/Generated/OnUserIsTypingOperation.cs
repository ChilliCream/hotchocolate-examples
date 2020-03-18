using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class OnUserIsTypingOperation
        : IOperation<IOnUserIsTyping>
    {
        public string Name => "onUserIsTyping";

        public IDocument Document => Queries.Default;

        public OperationKind Kind => OperationKind.Subscription;

        public Type ResultType => typeof(IOnUserIsTyping);

        public IReadOnlyList<VariableValue> GetVariableValues()
        {
            return Array.Empty<VariableValue>();
        }
    }
}
