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
    public partial class OnUserIsTypingResultParser
        : JsonResultParserBase<IOnUserIsTyping>
    {
        private readonly IValueSerializer _iDSerializer;

        public OnUserIsTypingResultParser(IValueSerializerCollection serializerResolver)
        {
            if (serializerResolver is null)
            {
                throw new ArgumentNullException(nameof(serializerResolver));
            }
            _iDSerializer = serializerResolver.Get("ID");
        }

        protected override IOnUserIsTyping ParserData(JsonElement data)
        {
            return new OnUserIsTyping
            (
                ParseOnUserIsTypingRecipient(data, "recipient")
            );

        }

        private global::Client.IHasPersonId ParseOnUserIsTypingRecipient(
            JsonElement parent,
            string field)
        {
            JsonElement obj = parent.GetProperty(field);

            return new HasPersonId
            (
                DeserializeID(obj, "id")
            );
        }

        private string DeserializeID(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (string)_iDSerializer.Deserialize(value.GetString())!;
        }
    }
}
