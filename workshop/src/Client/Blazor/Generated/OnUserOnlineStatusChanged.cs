using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class OnUserOnlineStatusChanged
        : IOnUserOnlineStatusChanged
    {
        public OnUserOnlineStatusChanged(
            global::Client.IHasPersonId onOnline)
        {
            OnOnline = onOnline;
        }

        public global::Client.IHasPersonId OnOnline { get; }
    }
}
