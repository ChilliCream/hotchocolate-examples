using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class DirectionValueSerializer
        : IValueSerializer
    {
        public string Name => "Direction";

        public ValueKind Kind => ValueKind.Enum;

        public Type ClrType => typeof(Direction);

        public Type SerializationType => typeof(string);

        public object? Serialize(object? value)
        {
            if (value is null)
            {
                return null;
            }

            var enumValue = (Direction)value;

            switch(enumValue)
            {
                case Direction.Incoming:
                    return "INCOMING";
                case Direction.Outgoing:
                    return "OUTGOING";
                default:
                    throw new NotSupportedException();
            }
        }

        public object? Deserialize(object? serialized)
        {
            if (serialized is null)
            {
                return null;
            }

            var stringValue = (string)serialized;

            switch(stringValue)
            {
                case "INCOMING":
                    return Direction.Incoming;
                case "OUTGOING":
                    return Direction.Outgoing;
                default:
                    throw new NotSupportedException();
            }
        }

    }
}
