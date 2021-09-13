using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Server.Ui.Voyager;
using GraphqlDemo.Core.Domain;
using GraphqlDemo.Data;
using GraphqlDemo.Web.Graphql;
using GraphqlDemo.Web.Graphql.Commands;
using GraphqlDemo.Web.Graphql.Commands.Query;
using GraphqlDemo.Web.Graphql.Platforms;
using GraphqlDemo.Web.Graphql.Platforms.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GraphqlDemo
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<Db>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("Default")));

            services
                .AddGraphQLServer()
                .AddFiltering()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddType<PlatformType>()
                .AddType<CommandType>()
                .AddInMemorySubscriptions();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets();
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });

            app.UseGraphQLVoyager(new GraphQLVoyagerOptions
            {
                GraphQLEndPoint = "/graphql",
                Path = "/graphql-voyager"
            });
        }
    }
}
