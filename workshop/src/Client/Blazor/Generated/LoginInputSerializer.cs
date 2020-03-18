using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class LoginInputSerializer
        : IInputSerializer
    {
        private bool _needsInitialization = true;
        private IValueSerializer? _stringSerializer;

        public string Name { get; } = "LoginInput";

        public ValueKind Kind { get; } = ValueKind.InputObject;

        public Type ClrType => typeof(LoginInput);

        public Type SerializationType => typeof(IReadOnlyDictionary<string, object>);

        public void Initialize(IValueSerializerCollection serializerResolver)
        {
            if (serializerResolver is null)
            {
                throw new ArgumentNullException(nameof(serializerResolver));
            }
            _stringSerializer = serializerResolver.Get("String");
            _needsInitialization = false;
        }

        public object? Serialize(object? value)
        {
            if (_needsInitialization)
            {
                throw new InvalidOperationException(
                    $"The serializer for type `{Name}` has not been initialized.");
            }

            if (value is null)
            {
                return null;
            }

            var input = (LoginInput)value;
            var map = new Dictionary<string, object?>();

            if (input.ClientMutationId.HasValue)
            {
                map.Add("clientMutationId", SerializeNullableString(input.ClientMutationId.Value));
            }

            if (input.Email.HasValue)
            {
                map.Add("email", SerializeNullableString(input.Email.Value));
            }

            if (input.Password.HasValue)
            {
                map.Add("password", SerializeNullableString(input.Password.Value));
            }

            return map;
        }

        private object? SerializeNullableString(object? value)
        {
            if (value is null)
            {
                return null;
            }


            return _stringSerializer!.Serialize(value);
        }

        public object? Deserialize(object? value)
        {
            throw new NotSupportedException(
                "Deserializing input values is not supported.");
        }
    }
}
