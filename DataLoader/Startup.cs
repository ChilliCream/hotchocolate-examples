using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using HotChocolate.AspNetCore;
using HotChocolate;
using HotChocolate.Execution.Configuration;
using MongoDB.Driver;
using HotChocolate.Utilities;
using MongoDB.Bson;

namespace HotChocolate.Examples.Paging
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // setup type conversion for object id
            TypeConversion.Default.Register<string, ObjectId>(from => ObjectId.Parse(from));
            TypeConversion.Default.Register<ObjectId, string>(from => from.ToString());

            // setup the repositories
            services.AddSingleton<IMongoClient>(new MongoClient("mongodb://127.0.0.1:27017"));
            services.AddSingleton<IMongoDatabase>(s => s.GetRequiredService<IMongoClient>().GetDatabase("PagingDemo"));
            services.AddSingleton<IMongoCollection<Message>>(s => s.GetRequiredService<IMongoDatabase>().GetCollection<Message>("messages"));
            services.AddSingleton<IMongoCollection<User>>(s => s.GetRequiredService<IMongoDatabase>().GetCollection<User>("users"));
            services.AddSingleton<MessageRepository>();
            services.AddSingleton<UserRepository>();

            // this enables you to use DataLoader in your resolvers.
            services.AddDataLoaderRegistry();

            // Add GraphQL Services
            services.AddGraphQL(Schema.Create(c =>
            {
                c.RegisterQueryType<QueryType>();
                c.RegisterMutationType<MutationType>();
                c.RegisterExtendedScalarTypes();
            }),
            new QueryExecutionOptions { TracingPreference = TracingPreference.Always });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphQL();
            //app.UsePlayground();
        }
    }
}
