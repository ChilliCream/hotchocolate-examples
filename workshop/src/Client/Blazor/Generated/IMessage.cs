using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial interface IMessage
    {
        Direction Direction { get; }

        string Id { get; }

        global::Client.IParticipant Recipient { get; }

        global::Client.IParticipant Sender { get; }

        System.DateTimeOffset Sent { get; }

        string Text { get; }
    }
}
