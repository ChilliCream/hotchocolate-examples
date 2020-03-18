using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class Recipient
        : IRecipient
    {
        public Recipient(
            global::Client.IMessageConnection? messages, 
            string name, 
            System.Uri? imageUri, 
            bool isOnline, 
            System.DateTimeOffset lastSeen, 
            string id, 
            string email)
        {
            Messages = messages;
            Name = name;
            ImageUri = imageUri;
            IsOnline = isOnline;
            LastSeen = lastSeen;
            Id = id;
            Email = email;
        }

        public global::Client.IMessageConnection? Messages { get; }

        public string Name { get; }

        public System.Uri? ImageUri { get; }

        public bool IsOnline { get; }

        public System.DateTimeOffset LastSeen { get; }

        public string Id { get; }

        public string Email { get; }
    }
}
