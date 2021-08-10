using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleInjector;

namespace RoutingEngine.Webapp
{
    public class Startup
    {
        private readonly SwaggerApiDefinition swaggerApiDefinition;
        private readonly Container container = new Container();

        public Startup(IConfiguration configuration)
        {
            this.swaggerApiDefinition = new SwaggerApiDefinition("Routing Engine", "v1",
                $"{this.GetType().Assembly.GetName().Name}.xml");
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.ConfigureSwagger(this.swaggerApiDefinition);
            services.ConfigureSimpleInjector(this.container);
            services.AddHttpClient();
            RoutingEngineBootStrapper.InitializeContainer(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseSimpleInjector(this.container);
            //loggerFactory.AddFile("Logs/mylog-{Date}.txt");
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.EnableSwagger(this.swaggerApiDefinition);

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.UseMiddleware(container);

            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
            // });
            container.Verify();
        }
    }
}