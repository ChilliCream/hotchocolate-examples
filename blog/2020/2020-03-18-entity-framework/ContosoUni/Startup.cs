using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Execution.Configuration;
using HotChocolate.AspNetCore.Serialization;
using HotChocolate.ApolloFederation;
using HotChocolate.Types;
using HotChocolate.ApolloFederation.Types;
using HotChocolate.Execution;
using System.IO;

namespace ContosoUniversity
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public async void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SchoolContext>();
            services.AddHttpResponseFormatter<DefaultHttpResponseFormatter>();

            var gqlService = services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddFiltering()
                .AddSorting()
                .ModifyPagingOptions(opt => opt.IncludeTotalCount = true)
                .AddApolloFederation(FederationVersion.Federation26)
                .ExportDirective<OneOfDirectiveType>();

            var schema = await gqlService.BuildSchemaAsync();
            await File.WriteAllTextAsync("./schema.graphql", schema.Print());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            InitializeDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints
                    .MapGraphQL()
                    .WithOptions(new GraphQLServerOptions { Tool = { Enable = true } });
            });
        }

        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using (
                var serviceScope = app.ApplicationServices
                    .GetService<IServiceScopeFactory>()
                    .CreateScope()
            )
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<SchoolContext>();
                if (context.Database.EnsureCreated())
                {
                    var course = new Course
                    {
                        Credits = 10,
                        Title = "Object Oriented Programming 1"
                    };

                    context.Enrollments.Add(
                        new Enrollment
                        {
                            Course = course,
                            Student = new Student
                            {
                                FirstMidName = "Rafael",
                                LastName = "Foo",
                                EnrollmentDate = DateTime.UtcNow
                            }
                        }
                    );
                    context.Enrollments.Add(
                        new Enrollment
                        {
                            Course = course,
                            Student = new Student
                            {
                                FirstMidName = "Pascal",
                                LastName = "Bar",
                                EnrollmentDate = DateTime.UtcNow
                            }
                        }
                    );
                    context.Enrollments.Add(
                        new Enrollment
                        {
                            Course = course,
                            Student = new Student
                            {
                                FirstMidName = "Michael",
                                LastName = "Baz",
                                EnrollmentDate = DateTime.UtcNow
                            }
                        }
                    );
                    context.SaveChangesAsync();
                }
            }
        }
    }
}
