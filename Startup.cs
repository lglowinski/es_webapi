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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ExpertalSystem.Swagger;
using System.Text;
using ExpertalSystem.Authorization;

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
            services.AddCustomAuthorization(Configuration);

            services.AddControllers();
            services.AddSwagger();

            services.AddMvcCore(options =>
            {
                options.EnableEndpointRouting = false;
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
            }).AddNewtonsoftJson();

            services.AddMvc();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            BsonSerializer.RegisterSerializer<IBase>(new ImpliedImplementationInterfaceSerializer<IBase, BaseEntity>(BsonSerializer.LookupSerializer<BaseEntity>()));
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly()).AsImplementedInterfaces();
            builder.AddMongo();
            builder.AddRepository<User>("Users");
            builder.AddRepository<Problem>("Problems");
            builder.AddRepository<Question>("Questions");
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
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMvc();
            app.UseSwagger();

            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("v1/swagger.json", "v1");
            });

            app.UseRouting();
            app.UseAuthorization();
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
