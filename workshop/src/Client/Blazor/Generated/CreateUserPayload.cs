using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class CreateUserPayload
        : ICreateUserPayload
    {
        public CreateUserPayload(
            global::Client.IUser user)
        {
            User = user;
        }

        public global::Client.IUser User { get; }
    }
}
