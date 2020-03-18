using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial interface IRecipient
        : IPerson
    {
        global::Client.IMessageConnection? Messages { get; }
    }
}
