using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StrawberryShake;
using StrawberryShake.Configuration;
using StrawberryShake.Http;
using StrawberryShake.Http.Pipelines;
using StrawberryShake.Http.Subscriptions;
using StrawberryShake.Serializers;
using StrawberryShake.Transport;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public static partial class ChatClientServiceCollectionExtensions
    {
        private const string _clientName = "ChatClient";

        public static IOperationClientBuilder AddChatClient(
            this IServiceCollection serviceCollection)
        {
            if (serviceCollection is null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            serviceCollection.AddSingleton<IChatClient, ChatClient>();

            serviceCollection.AddSingleton<IOperationExecutorFactory>(sp =>
                new HttpOperationExecutorFactory(
                    _clientName,
                    sp.GetRequiredService<IHttpClientFactory>().CreateClient,
                    sp.GetRequiredService<IClientOptions>().GetOperationPipeline<IHttpOperationContext>(_clientName),
                    sp.GetRequiredService<IClientOptions>().GetOperationFormatter(_clientName),
                    sp.GetRequiredService<IClientOptions>().GetResultParsers(_clientName)));

            serviceCollection.AddSingleton<IOperationStreamExecutorFactory>(sp =>
                new SocketOperationStreamExecutorFactory(
                    _clientName,
                    sp.GetRequiredService<ISocketConnectionPool>().RentAsync,
                    sp.GetRequiredService<ISubscriptionManager>(),
                    sp.GetRequiredService<IClientOptions>().GetOperationFormatter(_clientName),
                    sp.GetRequiredService<IClientOptions>().GetResultParsers(_clientName)));

            IOperationClientBuilder builder = serviceCollection.AddOperationClientOptions(_clientName)
                .AddValueSerializer(() => new DirectionValueSerializer())
                .AddValueSerializer(() => new LoginInputSerializer())
                .AddValueSerializer(() => new CreateUserInputSerializer())
                .AddResultParser(serializers => new PeopleResultParser(serializers))
                .AddResultParser(serializers => new GetPeopleAndRecipientResultParser(serializers))
                .AddResultParser(serializers => new RecipientByIdResultParser(serializers))
                .AddResultParser(serializers => new SendMessageResultParser(serializers))
                .AddResultParser(serializers => new SignInResultParser(serializers))
                .AddResultParser(serializers => new SignUpResultParser(serializers))
                .AddResultParser(serializers => new UserIsTypingResultParser(serializers))
                .AddResultParser(serializers => new OnMessageReceivedResultParser(serializers))
                .AddResultParser(serializers => new OnUserOnlineStatusChangedResultParser(serializers))
                .AddResultParser(serializers => new OnUserIsTypingResultParser(serializers))
                .AddOperationFormatter(serializers => new JsonOperationFormatter(serializers))
                .AddHttpOperationPipeline(builder => builder.UseHttpDefaultPipeline());

            serviceCollection.TryAddSingleton<ISubscriptionManager, SubscriptionManager>();
            serviceCollection.TryAddSingleton<IOperationExecutorPool, OperationExecutorPool>();
            serviceCollection.TryAddEnumerable(new ServiceDescriptor(
                typeof(ISocketConnectionInterceptor),
                typeof(MessagePipelineHandler),
                ServiceLifetime.Singleton));
            return builder;
        }

    }
}
