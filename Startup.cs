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
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.IO;

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
            services.AddCors(o => o.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

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
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                o.IncludeXmlComments(xmlPath);
            });
            
            services.AddMvcCore(options=>
            {
                options.EnableEndpointRouting = false;
            }).AddNewtonsoftJson();

            services.AddMvc();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            BsonSerializer.RegisterSerializer<IBase>(new ImpliedImplementationInterfaceSerializer<IBase, BaseEntity>(BsonSerializer.LookupSerializer<BaseEntity>()));
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
            app.UseCors();
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
