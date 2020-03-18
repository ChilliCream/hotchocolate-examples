using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class User
        : IUser
    {
        public User(
            string email)
        {
            Email = email;
        }

        public string Email { get; }
    }
}
