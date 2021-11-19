using System;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo.Gateway
{
    public class Startup
    {
        public const string Accounts = "accounts";
        public const string Inventory = "inventory";
        public const string Products = "products";
        public const string Reviews = "reviews";

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient(Accounts, c => c.BaseAddress = new Uri("http://localhost:8001/graphql"));
            services.AddHttpClient(Inventory, c => c.BaseAddress = new Uri("http://localhost:8002/graphql"));
            services.AddHttpClient(Products, c => c.BaseAddress = new Uri("http://localhost:8003/graphql"));
            services.AddHttpClient(Reviews, c => c.BaseAddress = new Uri("http://localhost:8004/graphql"));

            services
                .AddGraphQLServer()
                .AddQueryType(d => d.Name("Query"))
                .AddRemoteSchema(Accounts)
                .AddRemoteSchema(Inventory)
                .AddRemoteSchema(Products)
                .AddRemoteSchema(Reviews);
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
