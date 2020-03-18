using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class SendMessage1
        : ISendMessage
    {
        public SendMessage1(
            global::Client.ISendMessagePayload sendMessage)
        {
            SendMessage = sendMessage;
        }

        public global::Client.ISendMessagePayload SendMessage { get; }
    }
}
