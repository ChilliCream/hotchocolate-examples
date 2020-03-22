using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMessageRepository, InMemoryMessageRepository>();
            
            // services.AddInMemorySubscriptions();
            services.AddRedisSubscriptions(new )
            
            services.AddGraphQL(
                SchemaBuilder.New()
                    .AddQueryType<Query>()
                    .AddMutationType<Mutation>()
                    .AddSubscriptionType<Subscription>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseWebSockets();
            app.UseGraphQL();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }

    public class Query
    {
        public IQueryable<Message> GetMessages([Service]IMessageRepository repository) =>
            repository.GetMessages();
    }

    public class Mutation
    {
        public async Task<SendMessagePayload> SendMessageAsync(
            SendMessageInput input,
            [Service]IMessageRepository repository,
            [Service]ITopicEventSender sender)
        {
            var message = new Message(input.Text);

            await repository.AddMessageAsync(message);
            await sender.SendAsync("AllMessages", message);

            return new SendMessagePayload(message, input.ClientMutationId);
        }
    }

    public class Subscription
    {
        [SubscribeAndResolve]
        public async ValueTask<IAsyncEnumerable<Message>> OnMessageReceivedAsync(
            [Service]ITopicEventReceiver receiver,
            CancellationToken cancellationToken) =>
            await receiver.SubscribeAsync<string, Message>("AllMessages", cancellationToken);
    }

    public class SendMessageInput
    {
        public SendMessageInput(string text, string clientMutationId)
        {
            Text = text;
            ClientMutationId = clientMutationId;
        }

        public string Text { get; }

        public string ClientMutationId { get; }
    }

    public class SendMessagePayload
    {
        public SendMessagePayload(Message message, string clientMutationId)
        {
            Message = message;
            ClientMutationId = clientMutationId;
        }

        public Message Message { get; }

        public string ClientMutationId { get; }
    }

    public interface IMessageRepository
    {
        IQueryable<Message> GetMessages();

        ValueTask AddMessageAsync(Message message);
    }

    public class InMemoryMessageRepository
        : IMessageRepository
    {
        private List<Message> _messages = new List<Message>();

        public IQueryable<Message> GetMessages()
        {
            return _messages.AsQueryable();
        }

        public ValueTask AddMessageAsync(Message message)
        {
            _messages.Add(message);
            return default;
        }
    }

    public class Message
    {
        public Message(string text) : this(Guid.NewGuid(), text)
        {
        }

        public Message(Guid id, string text)
        {
            Id = id;
            Text = text;
        }

        public Guid Id { get; }

        public string Text { get; }
    }
}
