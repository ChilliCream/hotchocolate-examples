using Microsoft.Extensions.DependencyInjection;
using Chat.Server.Messages;
using Chat.Server.People;
using Chat.Server.Users;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace Chat.Server
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            string? connectionString = configuration.GetValue<string>("ConnectionString");

            return services
                .AddSingleton<IMongoDatabase>(sp =>
                    connectionString is null 
                        ? new MongoClient().GetDatabase("chat")
                        : new MongoClient(connectionString).GetDatabase("chat"))
                .AddSingleton<IUserRepository>(sp => 
                    new UserRepository(sp.GetRequiredService<IMongoDatabase>()
                        .GetCollection<User>(nameof(User))))
                .AddSingleton<IPersonRepository>(sp => 
                    new PersonRepository(sp.GetRequiredService<IMongoDatabase>()
                        .GetCollection<Person>(nameof(Person))))
                .AddSingleton<IMessageRepository>(sp => 
                    new MessageRepository(sp.GetRequiredService<IMongoDatabase>()
                        .GetCollection<Message>(nameof(Message))));
        }
    }
}