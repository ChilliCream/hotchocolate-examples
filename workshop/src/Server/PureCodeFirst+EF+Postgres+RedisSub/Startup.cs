using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Subscriptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using SlackClone.GraphQL.Mutations;
using SlackClone.GraphQL.Queries;
using SlackClone.GraphQL.Subscriptions;
using SlackClone.Models;
using StackExchange.Redis;

namespace SlackClone
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public static byte[] SharedSecret = Encoding.ASCII.GetBytes(
            "abcdefghijklmnopqrstuvwxyz1234567890");

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime.
        // Use this method to add services to the container.
        // For more information on how to configure your application,
        // visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            // configure jwt authentication
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(SharedSecret),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            if (context.HttpContext.Request.Query.ContainsKey("token"))
                            {
                                context.Token = context.HttpContext.Request.Query["token"];
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            var databaseUrl = Configuration["DATABASE_URL"];
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Prefer,
                TrustServerCertificate = true
            };

            var redisConfigurationOptions = new ConfigurationOptions
            {
                AllowAdmin = false,
                Ssl = false,
                Password = "p909446d3e9c3405a7e4e38876320a36ca80fa98668e44e7a49cdbabf02ad514e",
                EndPoints = {
                    {  "ec2-3-86-75-248.compute-1.amazonaws.com", 20849 }
                }
            };

            var conn = ConnectionMultiplexer.Connect(redisConfigurationOptions);

            // Adds GraphQL Schema
            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<SlackCloneDbContext>((sp, opt) =>
                   opt.UseNpgsql(builder.ToString())
                   .UseInternalServiceProvider(sp))
                //.AddInMemorySubscriptions()
                .AddRedisSubscriptions(conn)
                .AddGraphQL(sp =>
                   SchemaBuilder.New()
                   .AddServices(sp)
                   .AddQueryType(d => d.Name("Query"))
                   .AddType<UserQueries>()
                   .AddType<ChannelQueries>()
                   .AddMutationType(d => d.Name("Mutation"))
                   .AddType<UserMutations>()
                   .AddType<ChannelMutations>()
                   .AddSubscriptionType(d => d.Name("Subscription"))
                   .AddType<UserSubscriptions>()
                   .AddType<ChannelSubscriptions>()
                   .AddAuthorizeDirectiveType()
                   .Create());

            services.AddQueryRequestInterceptor((context, builder, ct) =>
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    builder.AddProperty(
                        "currentUserEmail",
                        context.User.FindFirst(ClaimTypes.Email).Value);
                }
                return Task.CompletedTask;
            });
        }

        // This method gets called by the runtime.
        // Use this method to configure the HTTP request pipeline.
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
            app.UseHttpsRedirection();

            // Adds GraphQL Service and Playground
            app.UseGraphQL()
                .UsePlayground();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("GraphQL Server Launched");
                });
            });
        }
    }
}