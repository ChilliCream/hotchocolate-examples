using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace raven;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IDocumentStore>(
            _ => new DocumentStore { Urls = new[] { "http://localhost:8080" }, Database = "Test" }
                .Initialize());

        services
            .AddGraphQLServer()
            .AddRavenFiltering()
            .AddRavenProjections()
            .AddRavenSorting()
            .AddRavenPagingProviders()
            .AddMutationConventions()
            .AddQueryType<Query>()
            .AddTypeExtension<PersonExtension>()
            .AddMutationType<Mutation>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints => { endpoints.MapGraphQL(); });
    }
}
