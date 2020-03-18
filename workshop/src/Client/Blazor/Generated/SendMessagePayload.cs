using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class SendMessagePayload
        : ISendMessagePayload
    {
        public SendMessagePayload(
            global::Client.IMessage message)
        {
            Message = message;
        }

        public global::Client.IMessage Message { get; }
    }
}
