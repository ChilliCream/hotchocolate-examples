using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class GetPeopleAndRecipient
        : IGetPeopleAndRecipient
    {
        public GetPeopleAndRecipient(
            global::Client.IPersonConnection? people, 
            global::Client.IRecipient personById)
        {
            People = people;
            PersonById = personById;
        }

        public global::Client.IPersonConnection? People { get; }

        public global::Client.IRecipient PersonById { get; }
    }
}
