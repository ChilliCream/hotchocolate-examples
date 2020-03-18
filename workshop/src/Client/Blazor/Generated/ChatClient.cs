using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class ChatClient
        : IChatClient
    {
        private const string _clientName = "ChatClient";

        private readonly global::StrawberryShake.IOperationExecutor _executor;
        private readonly global::StrawberryShake.IOperationStreamExecutor _streamExecutor;

        public ChatClient(global::StrawberryShake.IOperationExecutorPool executorPool)
        {
            _executor = executorPool.CreateExecutor(_clientName);
            _streamExecutor = executorPool.CreateStreamExecutor(_clientName);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::Client.IPeople>> GetPeopleAsync(
            global::StrawberryShake.Optional<string> userId = default,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (userId.HasValue && userId.Value is null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return _executor.ExecuteAsync(
                new GetPeopleOperation { UserId = userId },
                cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::Client.IPeople>> GetPeopleAsync(
            GetPeopleOperation operation,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (operation is null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            return _executor.ExecuteAsync(operation, cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::Client.IGetPeopleAndRecipient>> GetPeopleAndRecipientAsync(
            global::StrawberryShake.Optional<string> userId = default,
            global::StrawberryShake.Optional<string> recipientId = default,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (userId.HasValue && userId.Value is null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (recipientId.HasValue && recipientId.Value is null)
            {
                throw new ArgumentNullException(nameof(recipientId));
            }

            return _executor.ExecuteAsync(
                new GetPeopleAndRecipientOperation
                {
                    UserId = userId, 
                    RecipientId = recipientId
                },
                cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::Client.IGetPeopleAndRecipient>> GetPeopleAndRecipientAsync(
            GetPeopleAndRecipientOperation operation,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (operation is null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            return _executor.ExecuteAsync(operation, cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::Client.IRecipientById>> GetRecipientAsync(
            global::StrawberryShake.Optional<string> recipientId = default,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (recipientId.HasValue && recipientId.Value is null)
            {
                throw new ArgumentNullException(nameof(recipientId));
            }

            return _executor.ExecuteAsync(
                new GetRecipientOperation { RecipientId = recipientId },
                cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::Client.IRecipientById>> GetRecipientAsync(
            GetRecipientOperation operation,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (operation is null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            return _executor.ExecuteAsync(operation, cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::Client.ISendMessage>> SendMessageAsync(
            global::StrawberryShake.Optional<string> recipientEmail = default,
            global::StrawberryShake.Optional<string> text = default,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (recipientEmail.HasValue && recipientEmail.Value is null)
            {
                throw new ArgumentNullException(nameof(recipientEmail));
            }

            if (text.HasValue && text.Value is null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            return _executor.ExecuteAsync(
                new SendMessageOperation
                {
                    RecipientEmail = recipientEmail, 
                    Text = text
                },
                cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::Client.ISendMessage>> SendMessageAsync(
            SendMessageOperation operation,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (operation is null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            return _executor.ExecuteAsync(operation, cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::Client.ISignIn>> SignInAsync(
            global::StrawberryShake.Optional<global::Client.LoginInput> signIn = default,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (signIn.HasValue && signIn.Value is null)
            {
                throw new ArgumentNullException(nameof(signIn));
            }

            return _executor.ExecuteAsync(
                new SignInOperation { SignIn = signIn },
                cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::Client.ISignIn>> SignInAsync(
            SignInOperation operation,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (operation is null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            return _executor.ExecuteAsync(operation, cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::Client.ISignUp>> SignUpAsync(
            global::StrawberryShake.Optional<global::Client.CreateUserInput> newUser = default,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (newUser.HasValue && newUser.Value is null)
            {
                throw new ArgumentNullException(nameof(newUser));
            }

            return _executor.ExecuteAsync(
                new SignUpOperation { NewUser = newUser },
                cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::Client.ISignUp>> SignUpAsync(
            SignUpOperation operation,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (operation is null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            return _executor.ExecuteAsync(operation, cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::Client.IUserIsTyping>> UserIsTypingAsync(
            global::StrawberryShake.Optional<string> writingTo = default,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (writingTo.HasValue && writingTo.Value is null)
            {
                throw new ArgumentNullException(nameof(writingTo));
            }

            return _executor.ExecuteAsync(
                new UserIsTypingOperation { WritingTo = writingTo },
                cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::Client.IUserIsTyping>> UserIsTypingAsync(
            UserIsTypingOperation operation,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (operation is null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            return _executor.ExecuteAsync(operation, cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IResponseStream<global::Client.IOnMessageReceived>> OnMessageReceivedAsync(
            global::System.Threading.CancellationToken cancellationToken = default)
        {

            return _streamExecutor.ExecuteAsync(
                new OnMessageReceivedOperation(),
                cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IResponseStream<global::Client.IOnMessageReceived>> OnMessageReceivedAsync(
            OnMessageReceivedOperation operation,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (operation is null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            return _streamExecutor.ExecuteAsync(operation, cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IResponseStream<global::Client.IOnUserOnlineStatusChanged>> OnUserOnlineStatusChangedAsync(
            global::System.Threading.CancellationToken cancellationToken = default)
        {

            return _streamExecutor.ExecuteAsync(
                new OnUserOnlineStatusChangedOperation(),
                cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IResponseStream<global::Client.IOnUserOnlineStatusChanged>> OnUserOnlineStatusChangedAsync(
            OnUserOnlineStatusChangedOperation operation,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (operation is null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            return _streamExecutor.ExecuteAsync(operation, cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IResponseStream<global::Client.IOnUserIsTyping>> OnUserIsTypingAsync(
            global::System.Threading.CancellationToken cancellationToken = default)
        {

            return _streamExecutor.ExecuteAsync(
                new OnUserIsTypingOperation(),
                cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IResponseStream<global::Client.IOnUserIsTyping>> OnUserIsTypingAsync(
            OnUserIsTypingOperation operation,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (operation is null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            return _streamExecutor.ExecuteAsync(operation, cancellationToken);
        }
    }
}
