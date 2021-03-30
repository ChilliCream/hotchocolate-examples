using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver.Core.Events;
using MongoDB.Bson;

namespace MongoDB
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                const string connectionString = "mongodb://localhost";
                var mongoConnectionUrl = new MongoUrl(connectionString);
                var mongoClientSettings = MongoClientSettings.FromUrl(mongoConnectionUrl);
                mongoClientSettings.ClusterConfigurator = cb =>
                {
                    // This will print the executed command to the console
                    cb.Subscribe<CommandStartedEvent>(e =>
                    {
                        Console.WriteLine($"{e.CommandName} - {e.Command.ToJson()}");
                    });
                };
                var client = new MongoClient(mongoClientSettings);
                var database = client.GetDatabase("test");
                return database.GetCollection<Person>("person");
            });

            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .EnableRelaySupport()
                // Registers the filter convention of MongoDB
                .AddMongoDbFiltering()
                // Registers the sorting convention of MongoDB
                .AddMongoDbSorting()
                // Registers the projection convention of MongoDB
                .AddMongoDbProjections();
        }
 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
