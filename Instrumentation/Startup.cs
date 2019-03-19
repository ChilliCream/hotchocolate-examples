using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Execution;
using HotChocolate.Execution.Instrumentation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DiagnosticAdapter;
using Microsoft.Extensions.Logging;

namespace log
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGraphQL(c =>
            {
                c.RegisterQueryType<Query>();
            });

            services.AddDiagnosticObserver<DiagnosticObserver>();

            services.AddLogging();
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

            app.UseRouting(routes =>
            {
                routes.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }

    public class Query
    {
        public string Hello() => "world";
    }

    public class DiagnosticObserver
        : IDiagnosticObserver
    {
        private readonly ILogger _logger;
        public DiagnosticObserver(ILogger<Program> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [DiagnosticName("HotChocolate.Execution.Query")]
        public void OnQuery(IQueryContext context)
        {
            // ... your code
        }

        [DiagnosticName("HotChocolate.Execution.Query.Start")]
        public void BeginQueryExecute(IQueryContext context)
        {
            _logger.LogInformation(context.Request.Query);
        }
    }
}
