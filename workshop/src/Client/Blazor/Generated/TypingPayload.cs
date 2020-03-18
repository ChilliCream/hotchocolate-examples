using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class TypingPayload
        : ITypingPayload
    {
        public TypingPayload(
            string? clientMutationId)
        {
            ClientMutationId = clientMutationId;
        }

        public string? ClientMutationId { get; }
    }
}
