using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NoFileProvider
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration config)
        {
            AppConfiguration = config;

            AppConfiguration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            string[] args = { "name=Tom" };
            var builder = new ConfigurationBuilder().AddCommandLine(args);
            AppConfiguration = builder.Build();

            var build = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string,string>
            {
                { "age","19"}
            });
            AppConfiguration = build.Build();

        }
        private IConfiguration AppConfiguration { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var java_dir = AppConfiguration["JAVA_HOME"] ?? "not set";
            app.Run(async context =>
            {
                context.Response.ContentType = "text/html;charset=utf-8";
                await context.Response.WriteAsync($"FirstName: {AppConfiguration["firsname"]} <br> name: {AppConfiguration["name"]} <br> age: {AppConfiguration["age"]}<br> {java_dir} ");
            });
        }
    }
}
