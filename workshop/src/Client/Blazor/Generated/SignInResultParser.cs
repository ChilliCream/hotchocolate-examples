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
    public partial class SignInResultParser
        : JsonResultParserBase<ISignIn>
    {
        private readonly IValueSerializer _stringSerializer;
        private readonly IValueSerializer _urlSerializer;
        private readonly IValueSerializer _booleanSerializer;
        private readonly IValueSerializer _dateTimeSerializer;
        private readonly IValueSerializer _iDSerializer;

        public SignInResultParser(IValueSerializerCollection serializerResolver)
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
        }

        protected override ISignIn ParserData(JsonElement data)
        {
            return new SignIn
            (
                ParseSignInLogin(data, "login")
            );

        }

        private global::Client.ILoginPayload ParseSignInLogin(
            JsonElement parent,
            string field)
        {
            JsonElement obj = parent.GetProperty(field);

            return new LoginPayload
            (
                ParseSignInLoginMe(obj, "me"),
                DeserializeString(obj, "scheme"),
                DeserializeString(obj, "token")
            );
        }

        private global::Client.IPerson ParseSignInLoginMe(
            JsonElement parent,
            string field)
        {
            JsonElement obj = parent.GetProperty(field);

            return new Person
            (
                DeserializeString(obj, "name"),
                DeserializeNullableUrl(obj, "imageUri"),
                DeserializeBoolean(obj, "isOnline"),
                DeserializeDateTime(obj, "lastSeen"),
                DeserializeID(obj, "id"),
                DeserializeString(obj, "email")
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
    }
}
