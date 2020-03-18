using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial interface IChatClient
    {
        Task<IOperationResult<global::Client.IPeople>> GetPeopleAsync(
            Optional<string> userId = default,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::Client.IPeople>> GetPeopleAsync(
            GetPeopleOperation operation,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::Client.IGetPeopleAndRecipient>> GetPeopleAndRecipientAsync(
            Optional<string> userId = default,
            Optional<string> recipientId = default,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::Client.IGetPeopleAndRecipient>> GetPeopleAndRecipientAsync(
            GetPeopleAndRecipientOperation operation,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::Client.IRecipientById>> GetRecipientAsync(
            Optional<string> recipientId = default,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::Client.IRecipientById>> GetRecipientAsync(
            GetRecipientOperation operation,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::Client.ISendMessage>> SendMessageAsync(
            Optional<string> recipientEmail = default,
            Optional<string> text = default,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::Client.ISendMessage>> SendMessageAsync(
            SendMessageOperation operation,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::Client.ISignIn>> SignInAsync(
            Optional<global::Client.LoginInput> signIn = default,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::Client.ISignIn>> SignInAsync(
            SignInOperation operation,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::Client.ISignUp>> SignUpAsync(
            Optional<global::Client.CreateUserInput> newUser = default,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::Client.ISignUp>> SignUpAsync(
            SignUpOperation operation,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::Client.IUserIsTyping>> UserIsTypingAsync(
            Optional<string> writingTo = default,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::Client.IUserIsTyping>> UserIsTypingAsync(
            UserIsTypingOperation operation,
            CancellationToken cancellationToken = default);

        global::System.Threading.Tasks.Task<global::StrawberryShake.IResponseStream<global::Client.IOnMessageReceived>> OnMessageReceivedAsync(
            CancellationToken cancellationToken = default);

        global::System.Threading.Tasks.Task<global::StrawberryShake.IResponseStream<global::Client.IOnMessageReceived>> OnMessageReceivedAsync(
            OnMessageReceivedOperation operation,
            CancellationToken cancellationToken = default);

        global::System.Threading.Tasks.Task<global::StrawberryShake.IResponseStream<global::Client.IOnUserOnlineStatusChanged>> OnUserOnlineStatusChangedAsync(
            CancellationToken cancellationToken = default);

        global::System.Threading.Tasks.Task<global::StrawberryShake.IResponseStream<global::Client.IOnUserOnlineStatusChanged>> OnUserOnlineStatusChangedAsync(
            OnUserOnlineStatusChangedOperation operation,
            CancellationToken cancellationToken = default);

        global::System.Threading.Tasks.Task<global::StrawberryShake.IResponseStream<global::Client.IOnUserIsTyping>> OnUserIsTypingAsync(
            CancellationToken cancellationToken = default);

        global::System.Threading.Tasks.Task<global::StrawberryShake.IResponseStream<global::Client.IOnUserIsTyping>> OnUserIsTypingAsync(
            OnUserIsTypingOperation operation,
            CancellationToken cancellationToken = default);
    }
}
