using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class Message
        : IMessage
    {
        public Message(
            Direction direction, 
            string id, 
            global::Client.IParticipant recipient, 
            global::Client.IParticipant sender, 
            System.DateTimeOffset sent, 
            string text)
        {
            Direction = direction;
            Id = id;
            Recipient = recipient;
            Sender = sender;
            Sent = sent;
            Text = text;
        }

        public Direction Direction { get; }

        public string Id { get; }

        public global::Client.IParticipant Recipient { get; }

        public global::Client.IParticipant Sender { get; }

        public System.DateTimeOffset Sent { get; }

        public string Text { get; }
    }
}
