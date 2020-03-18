using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class PersonConnection
        : IPersonConnection
    {
        public PersonConnection(
            global::System.Collections.Generic.IReadOnlyList<global::Client.IPerson>? nodes)
        {
            Nodes = nodes;
        }

        public global::System.Collections.Generic.IReadOnlyList<global::Client.IPerson>? Nodes { get; }
    }
}
