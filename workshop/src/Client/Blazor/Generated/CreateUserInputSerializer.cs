using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class CreateUserInputSerializer
        : IInputSerializer
    {
        private bool _needsInitialization = true;
        private IValueSerializer? _stringSerializer;
        private IValueSerializer? _urlSerializer;

        public string Name { get; } = "CreateUserInput";

        public ValueKind Kind { get; } = ValueKind.InputObject;

        public Type ClrType => typeof(CreateUserInput);

        public Type SerializationType => typeof(IReadOnlyDictionary<string, object>);

        public void Initialize(IValueSerializerCollection serializerResolver)
        {
            if (serializerResolver is null)
            {
                throw new ArgumentNullException(nameof(serializerResolver));
            }
            _stringSerializer = serializerResolver.Get("String");
            _urlSerializer = serializerResolver.Get("Url");
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

            var input = (CreateUserInput)value;
            var map = new Dictionary<string, object?>();

            if (input.ClientMutationId.HasValue)
            {
                map.Add("clientMutationId", SerializeNullableString(input.ClientMutationId.Value));
            }

            if (input.Email.HasValue)
            {
                map.Add("email", SerializeNullableString(input.Email.Value));
            }

            if (input.Image.HasValue)
            {
                map.Add("image", SerializeNullableUrl(input.Image.Value));
            }

            if (input.Name.HasValue)
            {
                map.Add("name", SerializeNullableString(input.Name.Value));
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
        private object? SerializeNullableUrl(object? value)
        {
            if (value is null)
            {
                return null;
            }


            return _urlSerializer!.Serialize(value);
        }

        public object? Deserialize(object? value)
        {
            throw new NotSupportedException(
                "Deserializing input values is not supported.");
        }
    }
}
