using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace Demo.Reviews
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton(ConnectionMultiplexer.Connect("localhost:7000"))
                .AddSingleton<ReviewRepository>()
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .InitializeOnStartup()
                .PublishSchemaDefinition(c => c
                    .SetName("reviews")
                    .IgnoreRootTypes()
                    .AddTypeExtensionsFromFile("./Stitching.graphql")
                    .PublishToRedis("Demo", sp => sp.GetRequiredService<ConnectionMultiplexer>()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
