using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class Participant
        : IParticipant
    {
        public Participant(
            string id, 
            string name, 
            bool isOnline)
        {
            Id = id;
            Name = name;
            IsOnline = isOnline;
        }

        public string Id { get; }

        public string Name { get; }

        public bool IsOnline { get; }
    }
}
