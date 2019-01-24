using System;
using System.IO;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Execution;
using HotChocolate.Execution.Configuration;
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

            services.AddRemoteQueryExecutor(b => b
                .SetSchemaName("customer")
                .SetSchema(File.ReadAllText("./Customer.graphql")));

            services.AddRemoteQueryExecutor(b => b
                .SetSchemaName("contract")
                .SetSchema(File.ReadAllText("./Contract.graphql"))
                .AddScalarType<DateTimeType>());

            services.AddGraphQL(Schema.Create(
                File.ReadAllText("./Stitching.graphql"),
                c =>
                {
                    c.RegisterType<DateTimeType>();

                    // you can add middlewars on a stitched schema just like on local schemas:
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

                    c.UseSchemaStitching();
                })
                .MakeExecutable(b => b.UseStitchingPipeline(
                    new QueryExecutionOptions
                    {
                        // this enables appollo tracing on the stitched schema
                        EnableTracing = true
                    })));
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
