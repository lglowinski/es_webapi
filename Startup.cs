using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Autofac;
using System;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.StaticFiles;
using ExpertalSystem.Mongo;
using ExpertalSystem.Domain;
using ExpertalSystem.Repositories;
using Autofac.Extensions.DependencyInjection;
using System.Reflection;

namespace ExpertalSystem
{
    public class Startup
    {
        private ILifetimeScope AutofacContainer { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo
                {
                    Description = "Expertal system API created for bachleor degree",
                    Title = "Expertal System API",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Email = "lukasz.glowinski@o2.pl",
                        Name = "Lukasz Glowinski",
                        Url = new Uri("https://lglowinski.pl")
                    }
                });
            });

            services.AddMvcCore(options=>
            {
                options.EnableEndpointRouting = false;
            }).AddNewtonsoftJson();

            services.AddMvc();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly()).AsImplementedInterfaces();
            builder.AddMongo();
            builder.AddRepository<User>("Users");
            builder.AddRepository<Rule>("Rules");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            app.UseSwagger();

            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("v1/swagger.json", "v1");
            });
            
            app.UseAuthorization();
            app.UseRouting();
            app.UseEndpoints(options =>
            {
                options.MapControllers();
            });
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                AutofacContainer.Dispose();
            });
        }
    }
}
