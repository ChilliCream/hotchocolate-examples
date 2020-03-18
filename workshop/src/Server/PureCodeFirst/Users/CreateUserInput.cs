using System;

namespace Chat.Server.Users
{
    public class CreateUserInput
    {
        public CreateUserInput(
            string name,
            string email,
            string password,
            Uri? image,
            string? clientMutationId)
        {
            Name = name;
            Email = email;
            Password = password;
            Image = image;
            ClientMutationId = clientMutationId;
        }

        public string Name { get; }

        public string Email { get; }

        public string Password { get; }

        public Uri? Image { get; }

        public string? ClientMutationId { get; }
    }
}
