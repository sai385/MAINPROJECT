
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace YourNamespace.ExtensionMethods
    {
        public static class Swaggersecure
        {
            public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Your API",
                        Version = "v1"
                    });

                    // Add JWT Authentication to Swagger
                    var securityScheme = new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Description = "Enter Bearer token",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    };

                    c.AddSecurityDefinition("Bearer", securityScheme);

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        securityScheme,
                        new string[] {}
                    }
                });
                });

                return services;
            }
        }
    }
