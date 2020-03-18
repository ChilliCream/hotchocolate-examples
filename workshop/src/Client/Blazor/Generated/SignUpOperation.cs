using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class SignUpOperation
        : IOperation<ISignUp>
    {
        public string Name => "signUp";

        public IDocument Document => Queries.Default;

        public OperationKind Kind => OperationKind.Mutation;

        public Type ResultType => typeof(ISignUp);

        public Optional<global::Client.CreateUserInput> NewUser { get; set; }

        public IReadOnlyList<VariableValue> GetVariableValues()
        {
            var variables = new List<VariableValue>();

            if (NewUser.HasValue)
            {
                variables.Add(new VariableValue("newUser", "CreateUserInput", NewUser.Value));
            }

            return variables;
        }
    }
}
