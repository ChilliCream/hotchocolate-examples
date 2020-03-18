using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using StrawberryShake;
using StrawberryShake.Configuration;
using StrawberryShake.Http;
using StrawberryShake.Http.Subscriptions;
using StrawberryShake.Transport;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class SendMessageResultParser
        : JsonResultParserBase<ISendMessage>
    {
        private readonly IValueSerializer _directionSerializer;
        private readonly IValueSerializer _iDSerializer;
        private readonly IValueSerializer _dateTimeSerializer;
        private readonly IValueSerializer _stringSerializer;
        private readonly IValueSerializer _booleanSerializer;

        public SendMessageResultParser(IValueSerializerCollection serializerResolver)
        {
            if (serializerResolver is null)
            {
                throw new ArgumentNullException(nameof(serializerResolver));
            }
            _directionSerializer = serializerResolver.Get("Direction");
            _iDSerializer = serializerResolver.Get("ID");
            _dateTimeSerializer = serializerResolver.Get("DateTime");
            _stringSerializer = serializerResolver.Get("String");
            _booleanSerializer = serializerResolver.Get("Boolean");
        }

        protected override ISendMessage ParserData(JsonElement data)
        {
            return new SendMessage1
            (
                ParseSendMessageSendMessage(data, "sendMessage")
            );

        }

        private global::Client.ISendMessagePayload ParseSendMessageSendMessage(
            JsonElement parent,
            string field)
        {
            JsonElement obj = parent.GetProperty(field);

            return new SendMessagePayload
            (
                ParseSendMessageSendMessageMessage(obj, "message")
            );
        }

        private global::Client.IMessage ParseSendMessageSendMessageMessage(
            JsonElement parent,
            string field)
        {
            JsonElement obj = parent.GetProperty(field);

            return new Message
            (
                DeserializeDirection(obj, "direction"),
                DeserializeID(obj, "id"),
                ParseSendMessageSendMessageMessageRecipient(obj, "recipient"),
                ParseSendMessageSendMessageMessageSender(obj, "sender"),
                DeserializeDateTime(obj, "sent"),
                DeserializeString(obj, "text")
            );
        }

        private global::Client.IParticipant ParseSendMessageSendMessageMessageRecipient(
            JsonElement parent,
            string field)
        {
            JsonElement obj = parent.GetProperty(field);

            return new Participant
            (
                DeserializeID(obj, "id"),
                DeserializeString(obj, "name"),
                DeserializeBoolean(obj, "isOnline")
            );
        }

        private global::Client.IParticipant ParseSendMessageSendMessageMessageSender(
            JsonElement parent,
            string field)
        {
            JsonElement obj = parent.GetProperty(field);

            return new Participant
            (
                DeserializeID(obj, "id"),
                DeserializeString(obj, "name"),
                DeserializeBoolean(obj, "isOnline")
            );
        }

        private Direction DeserializeDirection(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (Direction)_directionSerializer.Deserialize(value.GetString())!;
        }

        private string DeserializeID(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (string)_iDSerializer.Deserialize(value.GetString())!;
        }

        private System.DateTimeOffset DeserializeDateTime(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (System.DateTimeOffset)_dateTimeSerializer.Deserialize(value.GetString())!;
        }

        private string DeserializeString(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (string)_stringSerializer.Deserialize(value.GetString())!;
        }
        private bool DeserializeBoolean(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (bool)_booleanSerializer.Deserialize(value.GetBoolean())!;
        }
    }
}
