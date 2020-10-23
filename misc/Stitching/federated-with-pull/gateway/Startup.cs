using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Language;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            services.AddHttpClient(Accounts, c => c.BaseAddress = new Uri("http://localhost:5051/graphql"));
            services.AddHttpClient(Inventory, c => c.BaseAddress = new Uri("http://localhost:5052/graphql"));
            services.AddHttpClient(Products, c => c.BaseAddress = new Uri("http://localhost:5053/graphql"));
            services.AddHttpClient(Reviews, c => c.BaseAddress = new Uri("http://localhost:5054/graphql"));

            services
                .AddGraphQLServer()
                .AddRemoteSchema(Accounts, ignoreRootTypes: true)
                .AddRemoteSchema(Inventory, ignoreRootTypes: true)
                .AddRemoteSchema(Products, ignoreRootTypes: true)
                .AddRemoteSchema(Reviews, ignoreRootTypes: true)
                .AddTypeExtensionsFromString("type Query { }");
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
