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
    public partial class SignUpResultParser
        : JsonResultParserBase<ISignUp>
    {
        private readonly IValueSerializer _stringSerializer;

        public SignUpResultParser(IValueSerializerCollection serializerResolver)
        {
            if (serializerResolver is null)
            {
                throw new ArgumentNullException(nameof(serializerResolver));
            }
            _stringSerializer = serializerResolver.Get("String");
        }

        protected override ISignUp ParserData(JsonElement data)
        {
            return new SignUp
            (
                ParseSignUpCreateUser(data, "createUser")
            );

        }

        private global::Client.ICreateUserPayload ParseSignUpCreateUser(
            JsonElement parent,
            string field)
        {
            JsonElement obj = parent.GetProperty(field);

            return new CreateUserPayload
            (
                ParseSignUpCreateUserUser(obj, "user")
            );
        }

        private global::Client.IUser ParseSignUpCreateUserUser(
            JsonElement parent,
            string field)
        {
            JsonElement obj = parent.GetProperty(field);

            return new User
            (
                DeserializeString(obj, "email")
            );
        }

        private string DeserializeString(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (string)_stringSerializer.Deserialize(value.GetString())!;
        }
    }
}
