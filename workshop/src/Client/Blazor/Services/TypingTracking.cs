using System;
using System.Threading.Tasks;
using System.Timers;
using StrawberryShake;

namespace Client.Services
{
    public sealed class TypingTracking
    {
        private readonly IChatClient _chatClient;
        private Timer _throttling;
        private IRecipient? _recipient;

        public EventHandler<TypingEventArgs>? Typing;

        public TypingTracking(IChatClient chatClient)
        {
            _chatClient = chatClient;

            _throttling = new Timer(750);
            _throttling.Elapsed += CheckTypingTimer;
            _throttling.Stop();
        }

        public void Begin()
        {
            Task.Run(ReceiveMessagesAsync);
        }

        public void SetRecipient(IRecipient? recipient)
        {
            _throttling.Stop();
            _recipient = recipient;
        }

        private void CheckTypingTimer(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("the user stopped typing ...");

            _throttling.Stop();
            Typing?.Invoke(this, new TypingEventArgs(_recipient.Email, false));
        }

        private async Task ReceiveMessagesAsync()
        {
            Console.WriteLine("start listening for typing ...");

            IResponseStream<IOnUserIsTyping> messageStream =
                await _chatClient.OnUserIsTypingAsync();

            Console.WriteLine("started listening for typing ...");

            await foreach (IOperationResult<IOnUserIsTyping> response in messageStream)
            {
                Console.WriteLine("evaluating message ...");

                if (!response.HasErrors
                    && response.Data is { }
                    && _recipient is { }
                    && _recipient.Id == response.Data.Recipient.Id)
                {
                    Console.WriteLine("commiting message ...");
                    Typing?.Invoke(this, new TypingEventArgs(_recipient.Email, true));

                    _throttling.Stop();
                    _throttling.Start();
                }
            }
        }
    }

    public sealed class TypingEventArgs : EventArgs
    {
        public TypingEventArgs(string email, bool isTyping)
        {
            Email = email;
            IsTyping = isTyping;
        }

        public string Email { get; }

        public bool IsTyping { get; }
    }
}