using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class OnUserOnlineStatusChangedOperation
        : IOperation<IOnUserOnlineStatusChanged>
    {
        public string Name => "onUserOnlineStatusChanged";

        public IDocument Document => Queries.Default;

        public OperationKind Kind => OperationKind.Subscription;

        public Type ResultType => typeof(IOnUserOnlineStatusChanged);

        public IReadOnlyList<VariableValue> GetVariableValues()
        {
            return Array.Empty<VariableValue>();
        }
    }
}
