using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Chat.Server.Messages;
using Chat.Server.People;
using Chat.Server.Users;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Voyager;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Chat.Server
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureAuthenticationServices(services);

            services.AddCors();

            services
                .AddDbContext<ChatDbContext>()
                .AddDataLoaderRegistry()
                .AddInMemorySubscriptions()
                .AddGraphQL(sp =>
                    SchemaBuilder.New()
                        .AddServices(sp)
                        .AddQueryType(d => d.Name("Query"))
                        .AddType<PersonQueries>()
                        .AddMutationType(d => d.Name("Mutation"))
                        .AddType<MessageMutations>()
                        .AddType<PersonMutations>()
                        .AddType<UserMutations>()
                        .AddSubscriptionType(d => d.Name("Subscription"))
                        .AddType<MessageSubscriptions>()
                        .AddType<PersonSubscriptions>()
                        .AddType<MessageExtension>()
                        .AddType<PersonExtension>()
                        .AddAuthorizeDirectiveType()
                        .BindClrType<string, StringType>()
                        .BindClrType<Guid, IdType>()
                        .Create(),
                    new QueryExecutionOptions { ForceSerialExecution = true });

            services.AddQueryRequestInterceptor((context, builder, ct) =>
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    var personId =
                        Guid.Parse(context.User.FindFirst(WellKnownClaimTypes.UserId).Value);

                    builder.AddProperty(
                        "currentPersonId",
                        personId);

                    builder.AddProperty(
                        "currentUserEmail",
                        context.User.FindFirst(ClaimTypes.Email).Value);
                }
                return Task.CompletedTask;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(o => o
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());

            app.UseRouting();

            app.UseAuthentication();

            app.UseWebSockets();

            app.UseGraphQL()
                .UsePlayground()
                .UseVoyager();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
