using HotChocolate;
using HotChocolate.AspNetCore;
using Demo.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Contracts
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ContractStorage>();

            // Add GraphQL Services
            services.AddGraphQL(Schema.Create(c =>
            {
                c.RegisterQueryType<QueryType>();
                c.RegisterType<LifeInsuranceContractType>();
                c.RegisterType<SomeOtherContractType>();

                c.UseGlobalObjectIdentifier();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
