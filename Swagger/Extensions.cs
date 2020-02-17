using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ExpertalSystem.Swagger
{
    public static class Extensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
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
                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Pass valid jwt token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                o.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                     new OpenApiSecurityScheme
                     {
                        Reference = new OpenApiReference
                        {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                     },
                    new List<string>()
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                o.IncludeXmlComments(xmlPath);
            });
            return services;
        }
    }
}
