using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class SignUp
        : ISignUp
    {
        public SignUp(
            global::Client.ICreateUserPayload createUser)
        {
            CreateUser = createUser;
        }

        public global::Client.ICreateUserPayload CreateUser { get; }
    }
}
