using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class LoginPayload
        : ILoginPayload
    {
        public LoginPayload(
            global::Client.IPerson me, 
            string scheme, 
            string token)
        {
            Me = me;
            Scheme = scheme;
            Token = token;
        }

        public global::Client.IPerson Me { get; }

        public string Scheme { get; }

        public string Token { get; }
    }
}
