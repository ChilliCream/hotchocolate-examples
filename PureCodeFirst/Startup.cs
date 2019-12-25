using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HotChocolate;
using HotChocolate.AspNetCore;
using StarWars.Repositories;
using StarWars.Characters;
using StarWars.Reviews;
using HotChocolate.Subscriptions;

namespace StarWars
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add the custom services like repositories etc ...
            services.AddSingleton<ICharacterRepository, CharacterRepository>();
            services.AddSingleton<IReviewRepository, ReviewRepository>();

            // Add in-memory event provider
            services.AddInMemorySubscriptionProvider();

            // Add GraphQL Services
            services.AddGraphQL(sp => SchemaBuilder.New()
                .AddServices(sp)
                .AddQueryType(d => d.Name("Query"))
                .AddMutationType(d => d.Name("Mutation"))
                .AddSubscriptionType(d => d.Name("Subscription"))
                .AddType<CharacterQueries>()
                .AddType<ReviewQueries>()
                .AddType<ReviewMutations>()
                .AddType<ReviewSubscriptions>()
                .AddType<Human>()
                .AddType<Droid>()
                .AddType<Starship>()

                // Adds the authorize directive and
                // enable the authorization middleware.
                .AddAuthorizeDirectiveType()

                // .AddMutationType<MutationType>()
                // .AddSubscriptionType<SubscriptionType>()
                .Create());


            // Add Authorization Policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("HasCountry", policy =>
                    policy.RequireAssertion(context =>
                        context.User.HasClaim(c =>
                            (c.Type == ClaimTypes.Country))));
            });

            /*
            Note: uncomment this
            section in order to simulate a user that has a country claim and
            passes the configured authorization rule.

            services.AddQueryRequestInterceptor((ctx, builder, ct) =>
            {
                var identity = new ClaimsIdentity("abc");
                identity.AddClaim(new Claim(ClaimTypes.Country, "us"));
                ctx.User = new ClaimsPrincipal(identity);
                builder.SetProperty(nameof(ClaimsPrincipal), ctx.User);
                return Task.CompletedTask;
            });
            */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app
                .UseWebSockets()
                .UseGraphQL("/graphql");
        }
    }
}
