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
    public partial class RecipientByIdResultParser
        : JsonResultParserBase<IRecipientById>
    {
        private readonly IValueSerializer _stringSerializer;
        private readonly IValueSerializer _urlSerializer;
        private readonly IValueSerializer _booleanSerializer;
        private readonly IValueSerializer _dateTimeSerializer;
        private readonly IValueSerializer _iDSerializer;
        private readonly IValueSerializer _directionSerializer;

        public RecipientByIdResultParser(IValueSerializerCollection serializerResolver)
        {
            if (serializerResolver is null)
            {
                throw new ArgumentNullException(nameof(serializerResolver));
            }
            _stringSerializer = serializerResolver.Get("String");
            _urlSerializer = serializerResolver.Get("Url");
            _booleanSerializer = serializerResolver.Get("Boolean");
            _dateTimeSerializer = serializerResolver.Get("DateTime");
            _iDSerializer = serializerResolver.Get("ID");
            _directionSerializer = serializerResolver.Get("Direction");
        }

        protected override IRecipientById ParserData(JsonElement data)
        {
            return new RecipientById
            (
                ParseGetRecipientPersonById(data, "personById")
            );

        }

        private global::Client.IRecipient ParseGetRecipientPersonById(
            JsonElement parent,
            string field)
        {
            JsonElement obj = parent.GetProperty(field);

            return new Recipient
            (
                ParseGetRecipientPersonByIdMessages(obj, "messages"),
                DeserializeString(obj, "name"),
                DeserializeNullableUrl(obj, "imageUri"),
                DeserializeBoolean(obj, "isOnline"),
                DeserializeDateTime(obj, "lastSeen"),
                DeserializeID(obj, "id"),
                DeserializeString(obj, "email")
            );
        }

        private global::Client.IMessageConnection? ParseGetRecipientPersonByIdMessages(
            JsonElement parent,
            string field)
        {
            if (!parent.TryGetProperty(field, out JsonElement obj))
            {
                return null;
            }

            if (obj.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            return new MessageConnection
            (
                ParseGetRecipientPersonByIdMessagesNodes(obj, "nodes")
            );
        }

        private global::System.Collections.Generic.IReadOnlyList<global::Client.IMessage>? ParseGetRecipientPersonByIdMessagesNodes(
            JsonElement parent,
            string field)
        {
            if (!parent.TryGetProperty(field, out JsonElement obj))
            {
                return null;
            }

            if (obj.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            int objLength = obj.GetArrayLength();
            var list = new global::Client.IMessage[objLength];
            for (int objIndex = 0; objIndex < objLength; objIndex++)
            {
                JsonElement element = obj[objIndex];
                list[objIndex] = new Message
                (
                    DeserializeDirection(element, "direction"),
                    DeserializeID(element, "id"),
                    ParseGetRecipientPersonByIdMessagesNodesRecipient(element, "recipient"),
                    ParseGetRecipientPersonByIdMessagesNodesSender(element, "sender"),
                    DeserializeDateTime(element, "sent"),
                    DeserializeString(element, "text")
                );

            }

            return list;
        }

        private global::Client.IParticipant ParseGetRecipientPersonByIdMessagesNodesRecipient(
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

        private global::Client.IParticipant ParseGetRecipientPersonByIdMessagesNodesSender(
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

        private string DeserializeString(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (string)_stringSerializer.Deserialize(value.GetString())!;
        }

        private System.Uri? DeserializeNullableUrl(JsonElement obj, string fieldName)
        {
            if (!obj.TryGetProperty(fieldName, out JsonElement value))
            {
                return null;
            }

            if (value.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            return (System.Uri?)_urlSerializer.Deserialize(value.GetString())!;
        }

        private bool DeserializeBoolean(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (bool)_booleanSerializer.Deserialize(value.GetBoolean())!;
        }

        private System.DateTimeOffset DeserializeDateTime(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (System.DateTimeOffset)_dateTimeSerializer.Deserialize(value.GetString())!;
        }

        private string DeserializeID(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (string)_iDSerializer.Deserialize(value.GetString())!;
        }
        private Direction DeserializeDirection(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (Direction)_directionSerializer.Deserialize(value.GetString())!;
        }
    }
}
