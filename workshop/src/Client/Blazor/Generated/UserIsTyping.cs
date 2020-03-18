using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class UserIsTyping
        : IUserIsTyping
    {
        public UserIsTyping(
            global::Client.ITypingPayload typing)
        {
            Typing = typing;
        }

        public global::Client.ITypingPayload Typing { get; }
    }
}
