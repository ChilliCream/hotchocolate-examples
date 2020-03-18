using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class OnUserIsTyping
        : IOnUserIsTyping
    {
        public OnUserIsTyping(
            global::Client.IHasPersonId recipient)
        {
            Recipient = recipient;
        }

        public global::Client.IHasPersonId Recipient { get; }
    }
}
