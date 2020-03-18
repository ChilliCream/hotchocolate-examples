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
    public partial class UserIsTypingResultParser
        : JsonResultParserBase<IUserIsTyping>
    {
        private readonly IValueSerializer _stringSerializer;

        public UserIsTypingResultParser(IValueSerializerCollection serializerResolver)
        {
            if (serializerResolver is null)
            {
                throw new ArgumentNullException(nameof(serializerResolver));
            }
            _stringSerializer = serializerResolver.Get("String");
        }

        protected override IUserIsTyping ParserData(JsonElement data)
        {
            return new UserIsTyping
            (
                ParseUserIsTypingTyping(data, "typing")
            );

        }

        private global::Client.ITypingPayload ParseUserIsTypingTyping(
            JsonElement parent,
            string field)
        {
            JsonElement obj = parent.GetProperty(field);

            return new TypingPayload
            (
                DeserializeNullableString(obj, "clientMutationId")
            );
        }

        private string? DeserializeNullableString(JsonElement obj, string fieldName)
        {
            if (!obj.TryGetProperty(fieldName, out JsonElement value))
            {
                return null;
            }

            if (value.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            return (string?)_stringSerializer.Deserialize(value.GetString())!;
        }
    }
}
