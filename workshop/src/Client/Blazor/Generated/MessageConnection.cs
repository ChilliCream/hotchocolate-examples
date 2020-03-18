using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class MessageConnection
        : IMessageConnection
    {
        public MessageConnection(
            global::System.Collections.Generic.IReadOnlyList<global::Client.IMessage>? nodes)
        {
            Nodes = nodes;
        }

        public global::System.Collections.Generic.IReadOnlyList<global::Client.IMessage>? Nodes { get; }
    }
}
