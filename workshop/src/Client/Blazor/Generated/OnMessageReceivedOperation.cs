using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class OnMessageReceivedOperation
        : IOperation<IOnMessageReceived>
    {
        public string Name => "onMessageReceived";

        public IDocument Document => Queries.Default;

        public OperationKind Kind => OperationKind.Subscription;

        public Type ResultType => typeof(IOnMessageReceived);

        public IReadOnlyList<VariableValue> GetVariableValues()
        {
            return Array.Empty<VariableValue>();
        }
    }
}
