using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class RecipientById
        : IRecipientById
    {
        public RecipientById(
            global::Client.IRecipient personById)
        {
            PersonById = personById;
        }

        public global::Client.IRecipient PersonById { get; }
    }
}
