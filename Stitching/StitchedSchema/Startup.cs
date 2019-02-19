using System;
using System.IO;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Execution;
using HotChocolate.Execution.Configuration;
using HotChocolate.Resolvers;
using HotChocolate.Stitching;
using HotChocolate.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Stitching
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Setup the clients that shall be used to access the remote endpoints.
            services.AddHttpClient("customer", client =>
            {
                client.BaseAddress = new Uri("http://127.0.0.1:5050");
            });

            services.AddHttpClient("contract", client =>
            {
                client.BaseAddress = new Uri("http://127.0.0.1:5051");
            });

            services.AddSingleton<IQueryResultSerializer, JsonQueryResultSerializer>();

            services.AddStitchedSchema(builder => builder
                .AddSchemaFromHttp("customer")
                .AddSchemaFromHttp("contract")
                .AddExtensionsFromFile("./Extensions.graphql")
                .AddSchemaConfiguration(c =>
                {
                    c.Use(next => async context =>
                    {
                        await next(context);

                        // so just to prove that we can do anything we do with a local schema we are make all strings upper case.
                        if (context.Field.Type.NamedType() is StringType
                            && context.Result is string s)
                        {
                            context.Result = s.ToUpperInvariant();
                        }
                    });

                    c.Map(new FieldReference("Customer", "foo"),
                        next => context =>
                        {
                            var obj = context.Parent<OrderedDictionary>();
                            context.Result = obj["name"] + "_" + obj["id"];
                            return Task.CompletedTask;
                        });
                }));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphQL();
            app.UsePlayground();
        }
    }
}
