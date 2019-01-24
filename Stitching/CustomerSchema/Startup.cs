using HotChocolate;
using HotChocolate.AspNetCore;
using Demo.Customers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Customers
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<CustomerRepository>();

            // Add GraphQL Services
            services.AddGraphQL(Schema.Create(c =>
            {
                c.RegisterQueryType<QueryType>();
                c.UseGlobalObjectIdentifier();
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
